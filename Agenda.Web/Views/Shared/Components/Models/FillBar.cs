﻿namespace Agenda.Web.Views.Shared.Components.Models
{
    public class FillBar
    {
        /// <summary>
        /// Id único em caso de ter mais de 1 na mesma tela.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Título do componente.
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição do componente.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Valor/Texto que será exibido no centro do componente.
        /// </summary>
        public string ValorReal { get; set; }

        /// <summary>
        /// Valor que será usado para preenchimento da barra. Mínimo 0 | Máximo 100.
        /// </summary>
        public int ValorPorcentagem { get; set; }

        /// <summary>
        /// Cor da barra.
        /// </summary>
        public string BarColor { get; set; }

        /// <summary>
        /// Texto usado para colocar uma "orelha" no componente. Deixe em branco para não inserir.
        /// </summary>
        public string BadgeText { get; set; }

        /// <summary>
        /// Cor da "orelha". Deixa em branco para cor padrão do sistema (cinza escuro).
        /// </summary>
        public string BadgeColor { get; set; }

        /// <summary>
        /// Controller usada para atualizar o componente. Caso deixe em branco o componente não será atualizado.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action usada para atualizar o componente. Caso deixe em branco o componente não será atualizado.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Valor definido em milisegundos. Ex: 6000 = 6 segundos
        /// </summary>
        public int IntervaloAtualizacao { get; set; }
    }
}