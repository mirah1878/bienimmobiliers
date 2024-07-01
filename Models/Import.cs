using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Transactions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class Import
{
 private readonly ApplicationDbContext _dbContext;

    public Import(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ImportCsvToDatabase<T>(string table,IFormFile file, Func<CsvReader, T> mapFunc, CsvConfiguration? csvConfig = null) where T : class
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Le fichier est vide ou n'existe pas.");
        }

        if (csvConfig == null)
        {
            csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                BadDataFound = null,
                MissingFieldFound = null
            };
        }

        using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            csv.Read();
            csv.ReadHeader();

            var entities = new List<T>();

            while (csv.Read())
            {
                var entity = mapFunc(csv);
                entities.Add(entity);
            }

            // Bulk insert using raw SQL
            foreach (var entity in entities)
            {
                var sql = GetInsertSql<T>(table);
                var parameters = GetSqlParameters<T>(entity);
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
        }
    }

    private string GetInsertSql<T>(string table)
    {
        var tableName = table; // Assume que le nom de la classe correspond au nom de la table
        var properties = typeof(T).GetProperties();
        var valueParams = string.Join(", ", properties.Select(p => $"@{p.Name.ToLower()}"));
        return $"INSERT INTO {tableName} VALUES ({valueParams})";
    }

    private NpgsqlParameter[] GetSqlParameters<T>(T entity)
    {
        var properties = typeof(T).GetProperties();
        var parameters = new List<NpgsqlParameter>();
        foreach (var property in properties)
        {
            parameters.Add(new NpgsqlParameter($"@{property.Name.ToLower()}", property.GetValue(entity) ?? DBNull.Value));
        }

        return parameters.ToArray();
    }
    
    public void InsertDataCommission()
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                // type
                _dbContext.Database.ExecuteSqlRaw(@"
                    INSERT INTO type_de_bien(nom, commission) 
                    SELECT DISTINCT (type), commission FROM commission_temporaire");
                                      
    
                transaction.Commit(); // Commit la transaction
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Une erreur s'est produite lors de l'insertion des données : " + ex.Message);
                // La transaction sera rollback automatiquement car Commit() n'a pas été appelé
            }
        }
    }

    public void InsertDataFromBien()
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                   
                // region
                _dbContext.Database.ExecuteSqlRaw(@"
                    INSERT INTO region (nom)
                    SELECT DISTINCT region
                    FROM bien_temporaire");

                //proprietaire
                _dbContext.Database.ExecuteSqlRaw(@"
                    INSERT INTO proprietaire (tel)
                    SELECT DISTINCT proprietaire
                    FROM bien_temporaire");
                    
                                            
                _dbContext.Database.ExecuteSqlRaw(@"
                    INSERT INTO bien (reference,nom,description,loyer,id_type_de_bien,id_proprietaire,id_region)
                    SELECT 
                        tm.reference,
                        tm.nom,
                        tm.description,
                        tm.loyer_mensuel,
                        ty.id,
                        pr.id,
                        re.id
                    FROM 
                        bien_temporaire tm
                    JOIN 
                        proprietaire pr ON tm.proprietaire = pr.tel
                    JOIN 
                        region re ON tm.region = re.nom
                    JOIN 
                        type_de_bien ty ON tm.type = ty.nom"
                    );
    
                                                 
    
                transaction.Commit(); // Commit la transaction
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Une erreur s'est produite lors de l'insertion des données : " + ex.Message);
                // La transaction sera rollback automatiquement car Commit() n'a pas été appelé
            }
        }
    }
  
    
    public void InsertDataFromLocation()
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                //client
                _dbContext.Database.ExecuteSqlRaw(@"
                    INSERT INTO client (email)
                    SELECT DISTINCT client
                    FROM location_temporaire");

                _dbContext.Database.ExecuteSqlRaw(@"
                    INSERT INTO location (id_bien, id_client, duree, date_debut)
                        SELECT 
                            bi.id,
                            cl.id,
                            tm.duree_mois,
                            tm.date_debut
                        FROM
                            location_temporaire tm
                        JOIN
                            bien bi ON tm.reference = bi.reference
                        JOIN
                            client cl ON tm.client = cl.email;
                        "
                    );

                
                transaction.Commit(); // Commit la transaction
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'insertion des données : " + ex.Message);
                // La transaction sera rollback automatiquement car Commit() n'a pas été appelé
            }
        }
    }
    
}
