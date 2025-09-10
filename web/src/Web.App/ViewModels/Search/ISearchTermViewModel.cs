// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public interface ISearchTermViewModel
{
    string? Term { get; set; }
    string? EstablishmentId { get; set; }
    string Hint { get; }
}