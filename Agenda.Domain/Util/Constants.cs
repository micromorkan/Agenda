﻿namespace Agenda.Domain.Util
{
    public static class Constants
    {
        #region PROFILES CONSTANTS

        public const string PROFILE_REPRESENTANTE = "REPRESENTANTE";
        public const string PROFILE_ADMINISTRATIVO = "ADMINISTRATIVO";
        public const string PROFILE_GERENTE = "GERENTE";
        public const string PROFILE_DIRETOR = "DIRETOR";
        public const string PROFILE_ADVOGADO = "ADVOGADO";
        public const string PROFILE_TI = "TI";

        #endregion

        #region ACCESS CONSTANTS

        public const string GS_ERRORS_ACCESS = "GS_ERRORS_ACCESS";
        public const string GS_AUTH_ERROR = "GS_AUTH_ERROR";

        #endregion

        #region FLUENT VALIDATION CONSTANTS

        public const string FLUENT_SEARCH = "SEARCH";
        public const string FLUENT_INSERT = "INSERT";
        public const string FLUENT_UPDATE = "UPDATE";
        public const string FLUENT_DELETE = "DELETE";
        public const string FLUENT_CHECK = "CHECK";
        public const string FLUENT_CANCEL = "CANCEL";
        public const string FLUENT_AUTHENTICATE = "AUTHENTICATE";

        #endregion

        #region SYSTEM CONSTANTS
        
        public const string SYSTEM_CONN_STRING = "DefaultConnection";
        public const string SYSTEM_SETTINGS = "SystemSettings";
        public const string SYSTEM_SETTINGS_REGISTERSYSTEMLOG = "RegisterSystemLog";
        public const string SYSTEM_SETTINGS_REGISTERERRORLOG = "RegisterErrorLog";
        
        public const string SYSTEM_SUCCESS_MSG = "Operação realizada com sucesso!";
        public const string SYSTEM_ERROR_MSG = "Não foi possível executar esta ação. Contate a equipe de TI.";
        public const string SYSTEM_ERROR_ID = "O id informado não foi encontrado.";

        public const string SYSTEM_ERROR_KEY = "GS_ERROR";
        public const string SYSTEM_EXCEPTION_OBJ = "EXOBJECT";
        public static readonly string[] SYSTEM_IGNORE_AUDIT_TABLES = { "Contract", "ContractHistoric", "SystemLog", "ErrorLog" };
        public const string SYSTEM_LOG_INSERT = "INSERT";
        public const string SYSTEM_LOG_UPDATE = "UPDATE";
        public const string SYSTEM_LOG_DELETE = "DELETE";

        public const string SYSTEM_RGBA_WHITE = "rgba(255, 255, 255, 1)";
        public const string SYSTEM_RGBA_GREEN = "rgba(201, 242, 155, 1)";
        public const string SYSTEM_RGBA_ORANGE = "rgba(251, 192, 147, 1)";
        public const string SYSTEM_RGBA_BLUE = "rgba(54, 162, 235, 1)";
        public const string SYSTEM_RGBA_RED = "rgba(255, 99, 132, 1)";
        public const string SYSTEM_RGBA_PURPLE = "rgba(159, 90, 253, 1)";

        #endregion

        #region PROPOSAL CONSTANTS

        public const string PROPOSAL_STATUS_AC= "AGUARDANDO CONFERÊNCIA";
        public const string PROPOSAL_STATUS_PC= "PROPOSTA CONFERIDA";
        public const string PROPOSAL_STATUS_PF = "PROPOSTA FINALIZADA";
        public const string PROPOSAL_STATUS_CA = "PROPOSTA CANCELADA";

        #endregion

        #region CONTRACT CONSTANTS

        public const string CONTRACT_STATUS_AD = "AGUARDANDO DOCUMENTOS";
        public const string CONTRACT_STATUS_PA = "PARA ANALISE";
        public const string CONTRACT_STATUS_CA = "CONTRATO APROVADO";
        public const string CONTRACT_STATUS_CR = "CONTRATO REPROVADO";
        public const string CONTRACT_STATUS_CC = "CONTRATO CANCELADO";

        public const string CONTRACT_REPROVAL_R1 = "DOCUMENTOS INSUFICIENTES";
        public const string CONTRACT_REPROVAL_R2 = "DOCUMENTOS ILEGÍVEIS";
        public const string CONTRACT_REPROVAL_R3 = "DADOS DIVERGENTES";
        public const string CONTRACT_REPROVAL_R4 = "FALTA ASSINATURA";
        public const string CONTRACT_REPROVAL_R5 = "VÍDEO NÃO CONFERE";

        public const string CONTRACT_REASON_C1 = "CLIENTE SOLICITOU O CANCELAMENTO";
        public const string CONTRACT_REASON_C2 = "CLIENTE NÃO DISPONIBILIZOU OS DOCUMENTOS";
        public const string CONTRACT_REASON_C3 = "SEM CONTATO COM O CLIENTE";
        public const string CONTRACT_REASON_C4 = "REPRESENTANTE NÃO FAZ MAIS PARTE DA EMPRESA";
        public const string CONTRACT_REASON_C5 = "POSSÍVEL TENTATIVA DE FRAUDE";
        public const string CONTRACT_REASON_C6 = "OUTRO";

        #endregion
    }
}
