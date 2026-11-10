using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;

namespace InventorySystem1
{
    public static class Database
    {
        private static readonly string FolderPath =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "InventorySystem1");

        private static readonly string FilePath =
            Path.Combine(FolderPath, "users.json");

        private static readonly object FileLock = new object();

        private class UserRecord
        {
            public string FullName { get; set; } = "";
            public string Email { get; set; } = "";
            public string PasswordHash { get; set; } = "";
            public string PasswordSalt { get; set; } = "";
        }

        public static void Initialize()
        {
            lock (FileLock)
            {
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                if (!File.Exists(FilePath))
                    File.WriteAllText(FilePath, "[]");
            }
        }

        private static List<UserRecord> ReadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<UserRecord>();

            string json = File.ReadAllText(FilePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<UserRecord>();

            return JsonSerializer.Deserialize<List<UserRecord>>(json)
                   ?? new List<UserRecord>();
        }

        private static void SaveUsers(List<UserRecord> users)
        {
            string json = JsonSerializer.Serialize(
                users,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            string tempPath = FilePath + ".tmp";

            File.WriteAllText(tempPath, json);

            File.Move(tempPath, FilePath, true);
        }

        public static bool EmailExists(string email)
        {
            lock (FileLock)
            {
                List<UserRecord> users = ReadUsers();

                return users.Any(u =>
                    string.Equals(
                        u.Email,
                        email.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }
        }

        public static void Register(
            string fullName,
            string email,
            string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash;

            using (var derive = new Rfc2898DeriveBytes(
                password,
                salt,
                100000,
                HashAlgorithmName.SHA256))
            {
                hash = derive.GetBytes(32);
            }

            lock (FileLock)
            {
                List<UserRecord> users = ReadUsers();

                if (users.Any(u =>
                    string.Equals(
                        u.Email,
                        email.Trim(),
                        StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("Email is already registered.");
                }

                UserRecord newUser = new UserRecord
                {
                    FullName = fullName.Trim(),
                    Email = email.Trim(),
                    PasswordHash = Convert.ToBase64String(hash),
                    PasswordSalt = Convert.ToBase64String(salt)
                };

                users.Add(newUser);

                SaveUsers(users);
            }
        }

        public static bool Login(
            string email,
            string password,
            out string fullName)
        {
            fullName = "";

            lock (FileLock)
            {
                List<UserRecord> users = ReadUsers();

                UserRecord? user = users.FirstOrDefault(u =>
                    string.Equals(
                        u.Email,
                        email.Trim(),
                        StringComparison.OrdinalIgnoreCase));

                if (user == null)
                    return false;

                byte[] savedHash =
                    Convert.FromBase64String(user.PasswordHash);

                byte[] salt =
                    Convert.FromBase64String(user.PasswordSalt);

                using (var derive = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    100000,
                    HashAlgorithmName.SHA256))
                {
                    byte[] enteredHash = derive.GetBytes(32);

                    if (CryptographicOperations.FixedTimeEquals(
                        savedHash, enteredHash))
                    {
                        fullName = user.FullName;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}