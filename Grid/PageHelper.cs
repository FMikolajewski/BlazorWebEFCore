namespace BlazorWebAppEFCore.Grid;

// Mantiene el estado de paginación.
public class PageHelper : IPageHelper
{
    // Artículos en una página.
    public int PageSize { get; set; } = 20;

    // Página actual, basada en 1.
    public int Page { get; set; }

    // Existe la página anterior ?
    public bool HasPrev => Page > 1;

    // Número de página anterior.
    public int PrevPage => Page <= 1 ? Page : Page - 1;

    // Existe la página siguiente ?
    public bool HasNext => Page < PageCount;

    // Numero de página siguiente.
    public int NextPage => Page < PageCount ? Page + 1 : Page;

    // Total de artículos en todas las páginas.
    public int TotalItemCount { get; set; }

    // Elementos de la página actual (deben ser menores o iguales que PageSize).
    public int PageItems { get; set; }

    // Página actual, basada en 0.
    public int DbPage => Page - 1;

    // Cuántos registros omitir para iniciar la página actual.
    public int Skip => PageSize * DbPage;

    // Número total de páginas.
    public int PageCount => (TotalItemCount + PageSize - 1) / PageSize;
}
