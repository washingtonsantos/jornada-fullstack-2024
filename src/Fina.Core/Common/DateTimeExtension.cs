namespace Fina.Core.Common
{
    /// <summary>
    /// Método extensão para resolver Datas
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Retorna primeiro dia do mês conforme parâmetros informados
        /// </summary>
        /// <param name="date">data completa</param>
        /// <param name="year">ano</param>
        /// <param name="month">mês</param>
        /// <returns></returns>
        public static DateTime GetFirstDay(this DateTime date, int? year = null, int? month = null)
            => new (year ?? date.Year, month ?? date.Month, day: 1);

        /// <summary>
        /// Retorna último dia do mês conforme parâmetros informados
        /// </summary>
        /// <param name="date">data completa</param>
        /// <param name="year">ano</param>
        /// <param name="month">mês</param>
        /// <returns></returns>
        public static DateTime GetLastDay(this DateTime date, int? year = null, int? month = null)
            => new DateTime(year ?? date.Year, month ?? date.Month, day: 1)
            .AddMonths(1)
            .AddDays(-1);
    }
}
