using EncryptingMessages;
using System.Security.Cryptography;

string message = "Encrypt me";

KeyGenerator kg = new KeyGenerator();
RSAParameters publicKey = kg.GetPublicKey();
RSAParameters privateKey = kg.GetPrivateKey();

Console.WriteLine("Encrypting...");

var enBytes = MessageUtil.EncryptMessage(publicKey, message);
Console.WriteLine("Encrypted");

Console.WriteLine("Decrypting...");
var result = MessageUtil.DecryptMessage(privateKey, enBytes);
Console.WriteLine("Decrypted");

Console.WriteLine("Result: " + result);