namespace Application.Models.Requests;

public class UpdateReviewDto
{

    // Contenido nulleable por si quiere modificar solo uno de los dos campos y solo actualizar los campos que llegaron
    public string? Content { get; set; }

    public int? Rating { get; set; }
}