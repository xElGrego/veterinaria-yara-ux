namespace veterinaria_yara_ux.application.models.dtos
{
    public class DtoResponse<T>
    {
        public T data { get; set; }
        public bool error { get; set; }
        public string msgRetorno { get; set; }
        public int code { get; set; }

        public DtoResponse(T data)
        {
            this.data = data;
            code = 200;
            msgRetorno = "OK";
            error = false;
        }
    }
}
