using System;
namespace SisVac.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string ToSentenceCase(this string str)
        {
            var parts = str.ToLower().Split(' ');
            var result = string.Empty;
            foreach(var part in parts)
            {
                part.ToCharArray()[0] =  part.ToUpper()[0];
                result+= part + " ";
            }
            return result.Trim();
        }
        //https://gist.github.com/gregorypilar/1569baf02f011ff25f27697638435aad
        public static bool IsValidDocument(this string cedula)
        {
            var sumaPar = 0;
            var sumaImpar = 0;
            var longitud = Convert.ToInt32(cedula.Length);
            /*Control de errores en el código*/
            try
            {
                //verificamos que la longitud del parametro sea igual a 11
                if (longitud == 11)
                {
                    var digitoVerificador = Convert.ToInt32(cedula.Substring(10, 1));
                    //recorremos en un ciclo for cada dígito de la cédula
                    for (var i = 9; i >= 0; i--)
                    {
                        //si el digito no es par multiplicamos por 2
                        var digito = Convert.ToInt32(cedula.Substring(i, 1));
                        if ((i % 2) != 0)
                        {
                            int digitoImpar = digito * 2;
                            //si el digito obtenido es mayor a 10, restamos 9
                            if (digitoImpar >= 10)
                            {
                                digitoImpar = digitoImpar - 9;
                            }
                            sumaImpar = sumaImpar + digitoImpar;
                        }
                        /*En los demás casos sumamos el dígito y lo aculamos 
                         en la variable */
                        else
                        {
                            sumaPar = sumaPar + digito;
                        }
                    }
                    //Declaración de variables a nivel de método o función.
                    /*Obtenemos el verificador restandole a 10 el modulo 10 de la suma total de los dígitos*/
                    var verificador = 10 - ((sumaPar + sumaImpar) % 10);
                    /*si el verificador es igual a 10 y el dígito verificador
                      es igual a cero o el verificador y el dígito verificador 
                      son iguales retorna verdadero*/
                    if (((verificador == 10) && (digitoVerificador == 0))
                         || (verificador == digitoVerificador))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                Console.WriteLine("No se pudo validar la cédula");
            }
            return false;
        }
    }
}
