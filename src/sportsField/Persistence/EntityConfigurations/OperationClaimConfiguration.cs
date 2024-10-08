using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Attiributes.Constants;
using Application.Features.Courts.Constants;
using Application.Features.CourtReservations.Constants;
using Application.Features.Retentions.Constants;
using Application.Features.Suspends.Constants;
using Application.Features.SFFiles.Constants;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region Attiributes CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AttiributesOperationClaims.Admin },
                new() { Id = ++lastId, Name = AttiributesOperationClaims.Read },
                new() { Id = ++lastId, Name = AttiributesOperationClaims.Write },
                new() { Id = ++lastId, Name = AttiributesOperationClaims.Create },
                new() { Id = ++lastId, Name = AttiributesOperationClaims.Update },
                new() { Id = ++lastId, Name = AttiributesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Courts CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CourtsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CourtsOperationClaims.Read },
                new() { Id = ++lastId, Name = CourtsOperationClaims.Write },
                new() { Id = ++lastId, Name = CourtsOperationClaims.Create },
                new() { Id = ++lastId, Name = CourtsOperationClaims.Update },
                new() { Id = ++lastId, Name = CourtsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CourtReservations CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Read },
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Write },
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Create },
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Update },
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Retentions CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = RetentionsOperationClaims.Admin },
                new() { Id = ++lastId, Name = RetentionsOperationClaims.Read },
                new() { Id = ++lastId, Name = RetentionsOperationClaims.Write },
                new() { Id = ++lastId, Name = RetentionsOperationClaims.Create },
                new() { Id = ++lastId, Name = RetentionsOperationClaims.Update },
                new() { Id = ++lastId, Name = RetentionsOperationClaims.Delete },
            ]
        );
        #endregion


        #region CustomOperationClaims CRUD

        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = SFFilesOperationClaims.Admin},
                new() { Id = ++lastId, Name = SFFilesOperationClaims.Create},
                new() { Id = ++lastId, Name = SFFilesOperationClaims.Delete},
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Rent},
                new() { Id = ++lastId, Name = CourtReservationsOperationClaims.Cancel},
                new() { Id = ++lastId, Name = CourtsOperationClaims.CeoItemsRead},
            ]);

        #endregion


        #region Suspends CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = SuspendsOperationClaims.Admin },
                new() { Id = ++lastId, Name = SuspendsOperationClaims.Read },
                new() { Id = ++lastId, Name = SuspendsOperationClaims.Write },
                new() { Id = ++lastId, Name = SuspendsOperationClaims.Create },
                new() { Id = ++lastId, Name = SuspendsOperationClaims.Update },
                new() { Id = ++lastId, Name = SuspendsOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
