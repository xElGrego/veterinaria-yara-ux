namespace veterinaria_yara_ux.application.models.dtos
{
    public class MsDtoResponseError
    {
        public int code { get; set; }
        public string? message { get; set; }
        public bool error { get; set; }
    }
}
