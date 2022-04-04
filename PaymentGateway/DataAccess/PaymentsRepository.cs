using System.Data.Common;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using PaymentsGateway.Domain;
using PaymentsGateway.Domain.Settings;

namespace DataAccess;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly IOptions<DbOptions> _dbOptions;
    
    public PaymentsRepository(IOptions<DbOptions> dbOptions)
    {
        _dbOptions = dbOptions ?? throw new ArgumentNullException(nameof(dbOptions));
    }
    public async Task<Payment?> Get(string paymentId, CancellationToken ct)
    {
        await using var connection = new SqliteConnection(_dbOptions.Value.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
            @"
                    SELECT json_data
                    FROM payments
                    WHERE payment_id = $payment_id
                ";
        command.Parameters.AddWithValue("$payment_id", paymentId);

        await using (var reader = await command.ExecuteReaderAsync(ct))
        {
            if(await reader.ReadAsync(ct))
            {
                var jsonData = reader.GetString(0);
                return JsonSerializer.Deserialize<Payment?>(jsonData);
            }
            else
            {
                return null;
            }
        }
    }

    public async Task Save(Payment payment, CancellationToken ct)
    {
        await using var connection = new SqliteConnection(_dbOptions.Value.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
            @"
                INSERT OR IGNORE INTO payments(payment_id, json_data) 
                VALUES ($payment_id, $data);
            ";
            
        command.Parameters.AddWithValue("$payment_id", payment.Id);
        command.Parameters.AddWithValue("$data", JsonSerializer.Serialize(payment));

        await command.ExecuteScalarAsync(ct);
    }
}