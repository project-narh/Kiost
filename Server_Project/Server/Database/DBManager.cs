using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using MySqlConnector.Logging;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata.Ecma335;
using NBitcoin.Secp256k1;

namespace Server.Database
{
    class DBManager
    {
        public static DBManager Instance { get; } = new DBManager();

        private ServerdbContext _context = new ServerdbContext();

        public ServerdbContext GetDbContext()
        {
            return _context;
        }

        public DBManager()
        {
            try
            {
                using (var connect = _context.CreateConnection())
                {
                    Console.WriteLine("MariaDB 연결 성공!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MariaDB 연결 실패: " + ex.Message);
            }
        }

        public void Init()
        {

        }
    }
}