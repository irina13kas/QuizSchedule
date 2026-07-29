using Application.DTOs.Bars;
using Application.DTOs.Djs;
using Application.DTOs.Fines;
using Application.DTOs.Games;
using Application.DTOs.Masters;
using Application.DTOs.Notifications;
using Application.DTOs.Photographers;
using Application.DTOs.Points;
using Application.DTOs.Quizman;
using Application.DTOs.Replacements;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Bar
            CreateMap<Bar, BarResponse>()
                .ForMember(dest => dest.BarId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BarName, opt => opt.MapFrom(src => src.Name));

            //Dj
            CreateMap<Dj, DjResponse>()
                .ForMember(dest => dest.DjId, opt => opt.MapFrom(src => src.Id));

            //FineHistory
            CreateMap<Fine, FinesHistoryItemDTO>()
                .ForMember(dest => dest.QuizmanId, opt => opt.MapFrom(src => src.QuizmanId))
                .ForMember(dest => dest.QuizmanName, opt => opt.MapFrom(src => src.Quizman.User.Name))
                .ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdminId))
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin.User.Name))
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId != null ? src.GameId : null))
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game!= null ? src.Game.Name: null));

            //Game
            CreateMap<Game, GameResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ResponsibleAdminName, opt => opt.MapFrom(
                    src => src.ResponsibleAdmin != null ? src.ResponsibleAdmin.User.Name : null))
                .ForMember(dest => dest.MasterName, opt => opt.MapFrom(
                    src => src.Master != null ? src.Master.Name : null))
                .ForMember(dest => dest.DjName, opt => opt.MapFrom(
                    src => src.Dj != null ? src.Dj.Name : null))
                .ForMember(dest => dest.PhotographerName, opt => opt.MapFrom(
                    src => src.Photographer != null ? src.Photographer.Name : null))
                .ForMember(dest => dest.BarName, opt => opt.MapFrom(
                    src => src.Bar != null ? src.Bar.Name : null));

            CreateMap<Game, GameWithParticipantsResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ResponsibleAdminName, opt => opt.MapFrom(
                    src => src.ResponsibleAdmin != null ? src.ResponsibleAdmin.User.Name : null))
                .ForMember(dest => dest.MasterName, opt => opt.MapFrom(
                    src => src.Master != null ? src.Master.Name : null))
                .ForMember(dest => dest.DjName, opt => opt.MapFrom(
                    src => src.Dj != null ? src.Dj.Name : null))
                .ForMember(dest => dest.PhotographerName, opt => opt.MapFrom(
                    src => src.Photographer != null ? src.Photographer.Name : null))
                .ForMember(dest => dest.BarName, opt => opt.MapFrom(
                    src => src.Bar != null ? src.Bar.Name : null));

            CreateMap<Game, GameResponseForParticipants>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ResponsibleAdminName, opt => opt.MapFrom(
                    src => src.ResponsibleAdmin != null ? src.ResponsibleAdmin.User.Name : null))
                .ForMember(dest => dest.BarName, opt => opt.MapFrom(
                    src => src.Bar != null ? src.Bar.Name : null));

            //Master
            CreateMap<Master, MasterResponse>()
                .ForMember(dest => dest.MasterId, opt => opt.MapFrom(src => src.Id));

            //Notification
            CreateMap<Notification, NotificationItemResponce>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(
                    src => src.Sender.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

            //Photographer
            CreateMap<Photographer, PhotographerResponse>()
                .ForMember(dest => dest.PhotographerId, opt => opt.MapFrom(src => src.Id));

            //PointsHistory
            CreateMap<Point, PointsHistoryItemDTO>()
                .ForMember(dest => dest.QuizmanId, opt => opt.MapFrom(src => src.QuizmanId))
                .ForMember(dest => dest.QuizmanName, opt => opt.MapFrom(src => src.Quizman.User.Name))
                .ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdminId))
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin.User.Name))
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId != null ? src.GameId : null))
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game != null ? src.Game.Name : null));

            //Quizman
            CreateMap<Quizman, QuizmanResponse>()
                .ForMember(dest => dest.QuizemanId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.QuizemanName, opt => opt.MapFrom(src => src.User.Name));

            //Replacement
            CreateMap<Replacement, ReplacementResponse>()
                .ForMember(dest => dest.QuizemanName, opt => opt.MapFrom(src => src.Quizeman.User.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.TakenAdminName, opt => opt.MapFrom(
                    src => src.TakenAdmin != null ? src.TakenAdmin.User.Name : null));
        }
    }
}
