using Application.Features.SFFiles.Commands.DeleteCourtImage;
using Application.Features.SFFiles.Commands.UpdateMainImage;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SFFiles.Profiles;
public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<CourtImage, DeletedCourtImageResponse>();

        CreateMap<CourtImage, UpdateMainImageCourtImageResponse>();
    }
}
