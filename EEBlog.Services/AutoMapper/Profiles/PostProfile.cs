﻿using AutoMapper;
using EEBlog.Entities.Concrete;
using EEBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Services.AutoMapper.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostAddDto, Post>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            //Add later
            //CreateMap<PostUpdate, Post>
            //CreateMap<Post, PostUpdateDto>
        }
    }
}