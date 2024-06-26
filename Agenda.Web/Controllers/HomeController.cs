﻿using Agenda.Domain.Entity;
using Agenda.Domain.Models;
using Agenda.Domain.Util;
using Agenda.Services.Interface;
using Agenda.Services.Service;
using Agenda.Web.Helpers;
using Agenda.Web.Util;
using Agenda.Web.ViewModel;
using Agenda.Web.Views.Shared.Components.Models;
using Agenda.Web.Views.Shared.Reports.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorEngine;
using RazorEngine.Templating;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Agenda.Web.Controllers
{
    [ServiceFilter(typeof(SecurityAttribute))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        #region INDEX
        public async Task<IActionResult> Index()
        {
            ComponentsVM dashboard = new ComponentsVM();

            //string userProfile = User.GetProfile();

            //if (userProfile != Constants.PROFILE_ADVOGADO && userProfile != Constants.PROFILE_TI)
            //{
            //    dashboard.LstChart.Add(await MontarChartContratoSemanal());
            //    dashboard.LstChart.Add(await MontarChartContratoMensal());

            //    if (userProfile == Constants.PROFILE_REPRESENTANTE)
            //    {
            //        dashboard.LstTile.Add(await MontarTileContratoCreditoMensal());
            //    }

            //    if (userProfile == Constants.PROFILE_DIRETOR)
            //    {
            //        var userList = (await _userService.GetAll(new Domain.Entity.User())).Object.ToList();

            //        userList = userList.Where(x => x.Active).ToList();

            //        for (int i = 0; i < userList.Count(); i++)
            //        {
            //            if (userList[i].Profile == Constants.PROFILE_REPRESENTANTE || userList[i].Profile == Constants.PROFILE_GERENTE)
            //            {
            //                dashboard.LstTile.Add(await MontarTileContratoMensal(i + 1, userList[i].Id));
            //            }
            //        }
            //    }

            //    if (userProfile == Constants.PROFILE_GERENTE)
            //    {                    
            //        var grupoId = Convert.ToInt32(User.GetGroupId());

            //        var userList = (await _userService.GetAll(new Domain.Entity.User())).Object.ToList();

            //        for (int i = 0; i < userList.Count(); i++)
            //        {
            //            if ((userList[i].Profile == Constants.PROFILE_REPRESENTANTE || userList[i].Profile == Constants.PROFILE_GERENTE)
            //                && userList[i].GroupUsers.Any(x=> x.GroupId == grupoId))
            //            {
            //                dashboard.LstTile.Add(await MontarTileContratoMensal(i + 1, userList[i].Id));
            //            }
            //        }
            //    }

            //}
            //else if (userProfile == Constants.PROFILE_ADVOGADO)
            //{
            //    return RedirectToAction("Index", "Contract");
            //}

            Views.Shared.Components.Models.Calendar calendario = new Views.Shared.Components.Models.Calendar();

            calendario.IdUsuario = 1;
            calendario.NomeUsuario = "Diego Andrade Sampaio";
            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 1,
                Titulo = "Evento 1",
                Descricao = "Descricao evento 1",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 2,
                Titulo = "Evento 2",
                Descricao = "Descricao evento 2",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(1)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(1).AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Cancelado
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 3,
                Titulo = "Evento 3",
                Descricao = "Descricao evento 3",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1).AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 4,
                Titulo = "Evento 4",
                Descricao = "Descricao evento 4",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-2).AddHours(2)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-2).AddHours(3)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 5,
                Titulo = "Evento 5",
                Descricao = "Descricao evento 5",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-2).AddHours(4)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-2).AddHours(5)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 6,
                Titulo = "Evento 6",
                Descricao = "Descricao evento 6",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-2).AddHours(6)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-2).AddHours(7)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            //calendario.Acoes.ControllerAtualizarCalendar = "Home";
            //calendario.Acoes.ActionAtualizarCalendar = "AtualizarCalendario";

            dashboard.Calendar = calendario;

            return View(dashboard);
        }

        public int CalculateOffset(DayOfWeek current, DayOfWeek desired)
        {
            int c = (int)current;
            int d = (int)desired;
            int offset = (7 - c + d) % 7;
            return offset == 0 ? 7 : offset;
        }

        #endregion

        #region EXEMPLOS COMPONENTES

        public async Task<IActionResult> Components()
        {
            ComponentsVM index = new ComponentsVM();

            #region FILLBAR

            List<FillBar> lstBar = new List<FillBar>();

            lstBar.Add(new FillBar
            {
                Id = 1,
                Titulo = "Consumo Mensal",
                Descricao = "Seu saldo expira em 10 dias!",
                ValorReal = "72%",
                ValorPorcentagem = 72,
                BarColor = "#0404B4",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 60000
            });

            lstBar.Add(new FillBar
            {
                Id = 2,
                Titulo = "Mensagens Enviadas",
                Descricao = "O pacote será renovado em 18 dias!",
                ValorReal = "132 mensagens",
                ValorPorcentagem = 43,
                BarColor = "#FF0040",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 60000
            });

            lstBar.Add(new FillBar
            {
                Id = 3,
                Titulo = "Total Gasto",
                Descricao = "Seu limite é de R$ 5.000,00",
                ValorReal = "R$ 4.450,00",
                ValorPorcentagem = 89,
                BarColor = "#74DF00",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 60000
            });

            lstBar.Add(new FillBar
            {
                Id = 4,
                Titulo = "Pré-Pago Semanal",
                Descricao = "Smart Pre Local + 500mb",
                ValorReal = "350mb",
                ValorPorcentagem = 65,
                BarColor = "#74DF00",
                BadgeText = "R$ 9,99",
                BadgeColor = "#000099",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 60000
            });

            index.LstFill = lstBar;

            #endregion

            #region CHART

            Agenda.Web.Views.Shared.Components.Models.Chart chart = new Agenda.Web.Views.Shared.Components.Models.Chart
            {
                Id = 1,
                Titulo = "1",
                Cores = new string[10] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)" },
                Textos = new string[10] { "Red", "Yellow", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue" },
                Valores = new int[10] { 15, 20, 30, 40, 50, 60, 70, 80, 90, 100 },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraHorizontal),
                Controller = "Home",
                Action = "AtualizarChart",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 60000
            };

            index.LstChart.Add(chart);

            Agenda.Web.Views.Shared.Components.Models.Chart chart2 = new Agenda.Web.Views.Shared.Components.Models.Chart
            {
                Id = 2,
                Titulo = "2",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { 15, 20, 30 },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraVertical),
                Controller = "Home",
                Action = "AtualizarChart2",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 60000
            };

            index.LstChart.Add(chart2);

            Agenda.Web.Views.Shared.Components.Models.Chart chart3 = new Agenda.Web.Views.Shared.Components.Models.Chart
            {
                Id = 3,
                Titulo = "3",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { 15, 20, 30 },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.Torta),
                Controller = "Home",
                Action = "AtualizarChart3",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 60000
            };

            index.LstChart.Add(chart3);

            #endregion

            #region TILE

            Tile tile = new Tile();

            tile.Id = 1;
            tile.BackgroundColor = "#FFF";
            tile.Icone = "fa-user";
            tile.Descricao = "Descrição";
            tile.Titulo = "Usuarios Ativos";
            tile.Valor = "15";
            tile.Controller = "Home";
            tile.Action = "AtualizarTile";
            tile.IntervaloAtualizacao = 60000;

            index.LstTile.Add(tile);

            Tile tile2 = new Tile();
            tile2.Id = 2;
            tile2.BackgroundColor = "#99ffcc";
            tile2.Icone = "fa-edit";
            tile2.Descricao = "Descrição";
            tile2.Titulo = "Casos abertos";
            tile2.Valor = "234";
            tile2.Controller = "Home";
            tile2.Action = "AtualizarTile2";
            tile2.IntervaloAtualizacao = 60000;

            index.LstTile.Add(tile2);

            Tile tile3 = new Tile();
            tile3.Id = 3;
            tile3.BackgroundColor = "#ffd9b3";
            tile3.Icone = "fa-folder";
            tile3.Descricao = "Descrição";
            tile3.Titulo = "Arquivos Pendentes";
            tile3.Valor = "56";
            tile3.Controller = "Home";
            tile3.Action = "AtualizarTile3";
            tile3.IntervaloAtualizacao = 60000;
            index.LstTile.Add(tile3);

            #endregion

            #region CALENDAR

            Views.Shared.Components.Models.Calendar calendario = new Views.Shared.Components.Models.Calendar();

            calendario.IdUsuario = 1;
            calendario.NomeUsuario = "Diego Andrade Sampaio";
            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 1,
                Titulo = "Evento 1",
                Descricao = "Descricao evento 1",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 2,
                Titulo = "Evento 2",
                Descricao = "Descricao evento 2",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(1)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(1).AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Cancelado
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 3,
                Titulo = "Evento 3",
                Descricao = "Descricao evento 3",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1).AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 4,
                Titulo = "Evento 4",
                Descricao = "Descricao evento 4",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1).AddHours(2)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1).AddHours(3)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Acoes.ControllerAtualizarCalendar = "Home";
            calendario.Acoes.ActionAtualizarCalendar = "AtualizarCalendario";

            index.Calendar = calendario;

            #endregion

            return View(index);
        }

        [HttpPost]
        public JsonResult AtualizarTile()
        {
            Tile tile = new Tile();

            tile.BackgroundColor = "";
            tile.Descricao = "Descrição";
            tile.Titulo = "Usuarios Cadastrados";
            tile.Valor = new Random().Next(6, 25).ToString();
            tile.IntervaloAtualizacao = 60000;

            return Json(tile);
        }

        [HttpPost]
        public JsonResult AtualizarTile2()
        {
            Tile tile2 = new Tile();
            tile2.BackgroundColor = "#99ffcc";
            tile2.Icone = "fa-database";
            tile2.Descricao = "Descrição";
            tile2.Titulo = "Casos abertos";
            tile2.Valor = new Random().Next(230, 250).ToString();
            tile2.IntervaloAtualizacao = 60000;

            return Json(tile2);
        }

        [HttpPost]
        public JsonResult AtualizarTile3()
        {
            Tile tile3 = new Tile();
            tile3.Id = 3;
            tile3.BackgroundColor = "#ffd9b3";
            tile3.Icone = "fa-edit";
            tile3.Descricao = "Descrição";
            tile3.Titulo = "Arquivos Pendentes";
            tile3.Valor = new Random().Next(40, 68).ToString();
            tile3.IntervaloAtualizacao = 60000;


            return Json(tile3);
        }

        [HttpPost]
        public JsonResult AtualizarChart()
        {
            var a = new Random().Next(1, 10);
            var b = new Random().Next(11, 20);
            var c = new Random().Next(21, 30);
            var d = new Random().Next(31, 40);
            var e = new Random().Next(41, 50);
            var f = new Random().Next(51, 60);
            var g = new Random().Next(61, 70);
            var h = new Random().Next(71, 80);
            var i = new Random().Next(81, 90);
            var j = new Random().Next(91, 100);

            Agenda.Web.Views.Shared.Components.Models.Chart chart = new Agenda.Web.Views.Shared.Components.Models.Chart
            {
                Id = 1,
                Titulo = "1",
                Cores = new string[10] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)" },
                Textos = new string[10] { "Red", "Yellow", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue" },
                Valores = new int[10] { j, i, h, g, f, e, d, c, b, a },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraHorizontal),
            };

            return Json(chart);
        }

        [HttpPost]
        public JsonResult AtualizarChart2()
        {
            var a = new Random().Next(1, 10);
            var b = new Random().Next(11, 20);
            var c = new Random().Next(21, 30);

            Agenda.Web.Views.Shared.Components.Models.Chart chart = new Agenda.Web.Views.Shared.Components.Models.Chart
            {
                Id = 2,
                Titulo = "2",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { a, b, c },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraVertical),
            };

            return Json(chart);
        }

        [HttpPost]
        public JsonResult AtualizarChart3()
        {
            var a = new Random().Next(1, 10);
            var b = new Random().Next(11, 20);
            var c = new Random().Next(21, 30);

            Agenda.Web.Views.Shared.Components.Models.Chart chart = new Agenda.Web.Views.Shared.Components.Models.Chart
            {
                Id = 3,
                Titulo = "3",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { a, b, c },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.Torta),
            };

            return Json(chart);
        }

        [HttpPost]
        public JsonResult AtualizarFill()
        {
            var a = new Random().Next(1, 100);

            FillBar chart = new FillBar
            {
                ValorPorcentagem = a,
            };

            return Json(chart);
        }

        #endregion

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}