using Dapper;
using MemberManagement.API.Application.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberManagement.API.Application.Queries;

public class MemberQueries : IMemberQueries
{
    private readonly string _connectionString;

    public MemberQueries(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<MemberDTO> GetMemberByIdAsync(Guid memberId)
    {
        const string query = @"SELECT Id, Name, Surnames, AddressStreet as Street, AddressNumber as Number, AddressAdditionalInformation as AdditionalInformation,
                                AddressZipCode as ZipCode, AddressTown as Town, AddressCountryName as CountryName, PhoneNumber, Email, BirthDate,
                                DNI, Type, ProfilePicture, CreatedAt, UpdatedAt
                        FROM Members
                        WHERE Id = @Id";

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QueryFirstOrDefaultAsync<MemberDTO>(query, new { Id = memberId });
    }

    public async Task<IEnumerable<MemberDTO>> GetAllMembersAsync()
    {
        const string query = @"SELECT Id, Name, Surnames,AddressStreet as Street, AddressNumber as Number, AddressAdditionalInformation as AdditionalInformation,
                                AddressZipCode as ZipCode, AddressTown as Town, AddressCountryName as CountryName, PhoneNumber, Email, BirthDate,
                                DNI, Type, ProfilePicture, CreatedAt, UpdatedAt
                        FROM Members";

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QueryAsync<MemberDTO>(query);
    }
}