using App_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Entidades;
//using System.Data.Objects.SqlClient;
using DWM.Models.Entidades;
using App_Dominio.Security;
using App_Dominio.Models;

namespace DWM.Models.Enumeracoes
{
    public class BindDropDownList
    {
        public IEnumerable<SelectListItem> Apto(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1] != null ? param[1].ToString() : "";

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from u in db.Unidades.AsEnumerable()
                            orderby u.torreId, u.unidadeId
                            select new SelectListItem()
                            {
                                Value = u.torreId + u.unidadeId.ToString(),
                                Text = u.torreId + u.unidadeId.ToString(),
                                Selected = (selectedValue != "" ? (u.torreId + u.unidadeId.ToString()).Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> AreaAtendimento(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            // params[3] -> indica se deve pesquisar somente as areas de atendimento do funcionário (usuário) corrente
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();


            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();
                
                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                if (param.Count() == 2)
                {
                    q = q.Union(from a in db.AreaAtendimentos.AsEnumerable()
                                orderby a.descricao
                                select new SelectListItem()
                                {
                                    Value = a.areaAtendimentoId.ToString(),
                                    Text = a.descricao,
                                    Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                                }).ToList();
                }
                else
                {
                    EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                    Sessao sessaoCorrente = security.getSessaoCorrente();

                    q = q.Union(from a in db.AreaAtendimentos.AsEnumerable()
                                where a.usuario1Id == sessaoCorrente.usuarioId ||
                                        a.usuario2Id == sessaoCorrente.usuarioId
                                orderby a.descricao
                                select new SelectListItem()
                                {
                                    Value = a.areaAtendimentoId.ToString(),
                                    Text = a.descricao,
                                    Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                                }).ToList();
                }

                return q;
            }

            
        }

        public IEnumerable<SelectListItem> Especialidade(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from e in db.EspecialidadeMedicas.AsEnumerable()
                            orderby e.descricao
                            select new SelectListItem()
                            {
                                Value = e.especialidadeId.ToString(),
                                Text = e.descricao,
                                Selected = (selectedValue != "" ? e.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> GruposAssociados(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from g in db.GrupoAssociados.AsEnumerable()
                            orderby g.descricao
                            select new SelectListItem()
                            {
                                Value = g.grupoAssociadoId.ToString(),
                                Text = g.descricao,
                                Selected = (selectedValue != "" ? g.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> AreaAtuacao(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from p in db.AreaAtuacaos.AsEnumerable()
                            orderby p.descricao
                            select new SelectListItem()
                            {
                                Value = p.areaAtuacaoId.ToString(),
                                Text = p.descricao,
                                Selected = (selectedValue != "" ? p.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> Cidade(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from p in db.Cidades.AsEnumerable()
                            orderby p.nome
                            select new SelectListItem()
                            {
                                Value = p.cidadeId.ToString(),
                                Text = p.nome,
                                Selected = (selectedValue != "" ? p.nome.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> TipoDocumento(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.TipoDocumentos.AsEnumerable()
                            orderby obj.descricao
                            select new SelectListItem()
                            {
                                Value = obj.tipoDocumentoId.ToString(),
                                Text = obj.descricao,
                                Selected = (selectedValue != "" ? obj.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> ChamadoMotivo(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.ChamadoMotivos.AsEnumerable()
                            orderby obj.descricao
                            select new SelectListItem()
                            {
                                Value = obj.chamadoMotivoId.ToString(),
                                Text = obj.descricao,
                                Selected = (selectedValue != "" ? obj.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> ChamadoStatus(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.ChamadoStatuss.AsEnumerable()
                            orderby obj.descricao
                            select new SelectListItem()
                            {
                                Value = obj.chamadoStatusId.ToString(),
                                Text = obj.descricao,
                                Selected = (selectedValue != "" ? obj.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> Condomino(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.Associados.AsEnumerable()
                            where obj.dt_fim == null
                            orderby obj.nome
                            select new SelectListItem()
                            {
                                Value = obj.associadoId.ToString(),
                                Text = obj.nome,
                                Selected = (selectedValue != "" ? obj.nome.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> Folder(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from p in db.DocFolders.AsEnumerable()
                            orderby p.descricao
                            select new SelectListItem()
                            {
                                Value = p.docFolderId.ToString(),
                                Text = p.descricao,
                                Selected = (selectedValue != "" ? p.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        #region DropDownList Relacao_Morador
        /// <summary>
        /// Retorna os valores constantes do estado civil para uma seleção 
        /// </summary>
        /// <param name="selectedValue">Item da lista que receberá o foco inicial</param>
        /// <param name="header">Informar o cabeçalho do dropdownlist. Exemplo: "Selecione...". Observação: Se não informado o dropdownlist não terá cabeçalho.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> Relacao_Morador(string selectedValue = "", string header = "")
        {
            List<SelectListItem> drp = new List<SelectListItem>() { 
                new SelectListItem() { Value = "Cônjuge", Text = "Cônjuge" }, 
                new SelectListItem() { Value = "Filho(a)", Text = "Filho(a)" }, 
                new SelectListItem() { Value = "Irmão(ã)", Text = "Irmão(ã)" }, 
                new SelectListItem() { Value = "Enteado(a)", Text = "Enteado(a)" }, 
                new SelectListItem() { Value = "Sobrinho(a)", Text = "Sobrinho(a)" }, 
                new SelectListItem() { Value = "Genro", Text = "Genro" }, 
                new SelectListItem() { Value = "Nora", Text = "Nora" }, 
                new SelectListItem() { Value = "Pai", Text = "Pai" }, 
                new SelectListItem() { Value = "Mãe", Text = "Mãe" }, 
                new SelectListItem() { Value = "Avô(ó)", Text = "Avô(ó)" }, 
                new SelectListItem() { Value = "Tio(a)", Text = "Tio(a)" }, 
                new SelectListItem() { Value = "Cunhado(a)", Text = "Cunhado(a)" }, 
                new SelectListItem() { Value = "Amigo(a)", Text = "Amigo(a)" }, 
                new SelectListItem() { Value = "Neto(a)", Text = "Neto(a)" }, 
                new SelectListItem() { Value = "Bisneto(a)", Text = "Bisneto(a)" }, 
                new SelectListItem() { Value = "Primo(a)", Text = "Primo(a)" }, 
                new SelectListItem() { Value = "Sogro(a)", Text = "Sogro(a)" }, 
                new SelectListItem() { Value = "Hóspede", Text = "Hóspede" }, 
                new SelectListItem() { Value = "Padrasto", Text = "Padrasto" }, 
                new SelectListItem() { Value = "Madrasta", Text = "Madrasta" }, 
                new SelectListItem() { Value = "Outros", Text = "Outros" }, 
            };

            return Funcoes.SelectListEnum(drp, selectedValue, header);
        }
        #endregion

        #region DropDownList Funcao_Funcionario
        /// <summary>
        /// Retorna os valores constantes das funções de um funcionário do condômino
        /// </summary>
        /// <param name="selectedValue">Item da lista que receberá o foco inicial</param>
        /// <param name="header">Informar o cabeçalho do dropdownlist. Exemplo: "Selecione...". Observação: Se não informado o dropdownlist não terá cabeçalho.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> Funcao_Funcionario(string selectedValue = "", string header = "")
        {
            List<SelectListItem> drp = new List<SelectListItem>() { 
                new SelectListItem() { Value = "Empregada domética", Text = "Empregada" }, 
                new SelectListItem() { Value = "Diarista", Text = "Diarista" }, 
                new SelectListItem() { Value = "Baby Sitter", Text = "Baby Sitter" }, 
                new SelectListItem() { Value = "Motorista", Text = "Motorista" }, 
                new SelectListItem() { Value = "Costureira", Text = "Costureira" }, 
                new SelectListItem() { Value = "Cabeleireira(o)", Text = "Cabeleireira(o)" }, 
                new SelectListItem() { Value = "Enfermeira", Text = "Enfermeira" }, 
                new SelectListItem() { Value = "Manicure", Text = "Manicure" }, 
                new SelectListItem() { Value = "Outros", Text = "Outros" }, 
            };

            return Funcoes.SelectListEnum(drp, selectedValue, header);
        }
        #endregion

        #region DropDownList Horario
        /// <summary>
        /// Retorna os valores constantes dos horários dos funcionário do condômino
        /// </summary>
        /// <param name="selectedValue">Item da lista que receberá o foco inicial</param>
        /// <param name="header">Informar o cabeçalho do dropdownlist. Exemplo: "Selecione...". Observação: Se não informado o dropdownlist não terá cabeçalho.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> Horario(string selectedValue = "", string header = "")
        {
            List<SelectListItem> drp = new List<SelectListItem>() { 
                new SelectListItem() { Value = "00:00", Text = "00:00" }, 
                new SelectListItem() { Value = "01:00", Text = "01:00" }, 
                new SelectListItem() { Value = "01:30", Text = "01:30" },
                new SelectListItem() { Value = "02:00", Text = "02:00" }, 
                new SelectListItem() { Value = "02:30", Text = "02:30" }, 
                new SelectListItem() { Value = "03:00", Text = "03:00" }, 
                new SelectListItem() { Value = "03:30", Text = "03:30" }, 
                new SelectListItem() { Value = "04:00", Text = "04:00" }, 
                new SelectListItem() { Value = "04:30", Text = "04:30" }, 
                new SelectListItem() { Value = "05:00", Text = "05:00" }, 
                new SelectListItem() { Value = "05:30", Text = "05:30" }, 
                new SelectListItem() { Value = "06:00", Text = "06:00" }, 
                new SelectListItem() { Value = "06:30", Text = "06:30" }, 
                new SelectListItem() { Value = "07:00", Text = "07:00" }, 
                new SelectListItem() { Value = "07:30", Text = "07:30" }, 
                new SelectListItem() { Value = "08:00", Text = "08:00" }, 
                new SelectListItem() { Value = "08:30", Text = "08:30" }, 
                new SelectListItem() { Value = "09:00", Text = "09:00" }, 
                new SelectListItem() { Value = "09:30", Text = "09:30" }, 
                new SelectListItem() { Value = "10:00", Text = "10:00" }, 
                new SelectListItem() { Value = "10:30", Text = "10:30" }, 
                new SelectListItem() { Value = "11:00", Text = "11:00" }, 
                new SelectListItem() { Value = "11:30", Text = "11:30" }, 
                new SelectListItem() { Value = "12:00", Text = "12:00" }, 
                new SelectListItem() { Value = "12:30", Text = "12:30" }, 
                new SelectListItem() { Value = "13:00", Text = "13:00" }, 
                new SelectListItem() { Value = "13:30", Text = "13:30" }, 
                new SelectListItem() { Value = "14:00", Text = "14:00" }, 
                new SelectListItem() { Value = "14:30", Text = "14:30" }, 
                new SelectListItem() { Value = "15:00", Text = "15:00" }, 
                new SelectListItem() { Value = "15:30", Text = "15:30" }, 
                new SelectListItem() { Value = "16:00", Text = "16:00" }, 
                new SelectListItem() { Value = "16:30", Text = "16:30" }, 
                new SelectListItem() { Value = "17:00", Text = "17:00" }, 
                new SelectListItem() { Value = "17:30", Text = "17:30" }, 
                new SelectListItem() { Value = "18:00", Text = "18:00" }, 
                new SelectListItem() { Value = "18:30", Text = "18:30" }, 
                new SelectListItem() { Value = "19:00", Text = "19:00" }, 
                new SelectListItem() { Value = "19:30", Text = "19:30" }, 
                new SelectListItem() { Value = "20:00", Text = "20:00" }, 
                new SelectListItem() { Value = "20:30", Text = "20:30" }, 
                new SelectListItem() { Value = "21:00", Text = "21:00" }, 
                new SelectListItem() { Value = "21:30", Text = "21:30" }, 
                new SelectListItem() { Value = "22:00", Text = "22:00" }, 
                new SelectListItem() { Value = "22:30", Text = "22:30" }, 
                new SelectListItem() { Value = "23:00", Text = "23:00" }, 
                new SelectListItem() { Value = "23:30", Text = "23:30" }, 
            };

            return Funcoes.SelectListEnum(drp, selectedValue, header);
        }
        #endregion

        #region DropDownList Periodo
        /// <summary>
        /// Retorna o período para processamento de datas
        /// </summary>
        /// <param name="selectedValue">Item da lista que receberá o foco inicial</param>
        /// <param name="header">Informar o cabeçalho do dropdownlist. Exemplo: "Selecione...". Observação: Se não informado o dropdownlist não terá cabeçalho.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> Periodo(string selectedValue = "", string header = "")
        {
            List<SelectListItem> drp = new List<SelectListItem>() { 
                new SelectListItem() { Value = "30", Text = "Últimos 30 dias" }, 
                new SelectListItem() { Value = "3", Text = "Últimos 3 meses" }, 
                new SelectListItem() { Value = "6", Text = "Últimos 6 meses" },
                new SelectListItem() { Value = "12", Text = "Últimos 12 meses" }, 
            };

            return Funcoes.SelectListEnum(drp, selectedValue, header);
        }
        #endregion
    }
}