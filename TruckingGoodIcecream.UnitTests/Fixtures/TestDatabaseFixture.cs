namespace TruckingGoodIcecream.UnitTests.Fixtures;

using System;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;
using Microsoft.Data.Sqlite;
using TruckingGoodIcecream.Core.Models.Entities;

public class TestDatabaseFixture
{
    public DataConnection CreateConnection()
    {
        // 1. Create unique in-memory connection
        var sqliteConnection = new SqliteConnection($"Data Source=InMemory_{Guid.NewGuid()};Mode=Memory;Cache=Shared");
        sqliteConnection.Open();

        // 2. Create the DataConnection explicitly via SQLiteTools
        var db = SQLiteTools.CreateDataConnection(
            sqliteConnection,
            SQLiteProvider.Microsoft
        );

        // 3. Create schema
        db.CreateTable<FlavorEntity>(tableOptions: TableOptions.CreateIfNotExists);
        return db;
    }
}