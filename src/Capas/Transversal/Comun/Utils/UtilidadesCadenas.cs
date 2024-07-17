using Microsoft.Extensions.Configuration;

namespace Transversal.Comun.Utils
{
  public static class UtilidadesCadenas
  {
    public static class Pruebas
    {
      public const string Digitos = "1234567890";
      public const string MaximoValorEntero = "2147483647";
      public const string MaximoValorEnteroLargo = "9223372036854775807";
      public const string EnteroConValorMayorAlMaximo = "2147483648";
      public const string EnteroConAnchoMayorAlMaximo = "11111111112";
      public const string MaximoValorMontoDecimalCadena = "999999999999999";
      public const string EnteroLargoConValorMayorAlMaximo = "9223372036854775808";
      public const string EnteroLargoConAnchoMayorAlMaximo = "92233720368547758079";
      public const string AlfabetoMinusculas = "abcdefghijklmnopqrstuvwxyz";
      public const string AlfabetoMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      public const string TextoConAncho50 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+";
      public const string TextoConAncho51 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*";
      public const string CorreoAlfabetoMinusculas = "abcdefghijklmnopqrstuvwxyz@abcdefghijklmnopqrstuvwxyz.com";
      public const string CorreoAlfabetoMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ@ABCDEFGHIJKLMNOPQRSTUVWXYZ.COM";
      public const string CorreoConAnchoMayorAlMaximo = "abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789_abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789@abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789.COM.CO";
      public const string TextoConAncho10 = "_aBcDeFgHi";
      public const string TextoConAncho11 = "_aBcDeFgHij";
      public const string TextoConAncho19 = "_aBcDeFgHi_aBcDeFgH";
      public const string TextoConAncho20 = "_aBcDeFgHi_aBcDeFgHi";
      public const string TextoConAncho30 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0";
      public const string TextoConAncho31 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0r";
      public const string TextoConAncho100 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+";
      public const string TextoConAncho101 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+2";
      public const string TextoConAncho255 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*";
      public const string TextoConAncho256 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*1";
      public const string TextoConAncho300 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 ";
      public const string TextoConAncho301 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8";
      public const string TextoConAncho450 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6";
      public const string TextoConAncho451 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 67";
      public const string TextoConAncho500 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+";
      public const string TextoConAncho501 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+1";
      public const string TextoConAncho5000 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+";
      public const string TextoConAncho5001 = "_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+*_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6_aBcDeFgHiJkLm-NoPqRsTuVwXyZ_0 1 2 3 4 5 6 7 8 9_+1";
      public const string PrecioUnitarioDecimalConAncho18 = "2147483647214748.36";
      public const string PrecioUnitarioDecimalConAncho19 = "21474836472147487.36";
      public const string NumeroEnteroLargoConAncho15 = "123456789012345";
      public const string NumeroEnteroLargoConAncho16 = "1234567890123456";
    }

    public const string RegexBooleano = @"^(0|1)$";
    public const string RegexEntero = @"^-?\d+$";
    public const string RegexFechaHora = @"^(19|20)[0-9]{2}[- /.]([0][1-9]|[1][0-2])[- /.](0[1-9]|1[0-9]|2[0-9]|3[0-1])[- /.](0[0-9]|1[0-9]|2[0-3])[:.](0|1|2|3|4|5)[0-9]{1}[:.](0|1|2|3|4|5)[0-9]{1}$";
    public const string RegexMoneda = @"^(AED|AFN|ALL|AMD|ANG|AOA|ARS|AUD|AWG|AZN|BAM|BBD|BDT|BGN|BHD|BIF|BMD|BND|BOB|BOV|BRL|BSD|BTN|BWP|BYR|BZD|CAD|CDF|CHE|CHF|CHW|CLF|CLP|CNY|COP|COU|CRC|CUC|CUP|CVE|CZK|DJF|DKK|DOP|DZD|EGP|ERN|ETB|EUR|FJD|FKP|GBP|GEL|GHS|GIP|GMD|GNF|GTQ|GYD|HKD|HNL|HRK|HTG|HUF|IDR|ILS|INR|IQD|IRR|ISK|JMD|JOD|JPY|KES|KGS|KHR|KMF|KPW|KRW|KWD|KYD|KZT|LAK|LBP|LKR|LRD|LSL|LYD|MAD|MDL|MGA|MKD|MMK|MNT|MOP|MRO|MUR|MVR|MWK|MXN|MXV|MYR|MZN|NAD|NGN|NIO|NOK|NPR|NZD|OMR|PAB|PEN|PGK|PHP|PKR|PLN|PYG|QAR|RON|RSD|RUB|RWF|SAR|SBD|SCR|SDG|SEK|SGD|SHP|SLL|SOS|SRD|SSP|STD|SVC|SYP|SZL|THB|TJS|TMT|TND|TOP|TRY|TTD|TWD|TZS|UAH|UGX|USD|USN|UYI|UYU|UZS|VES|VES7|VND|VUV|WST|XAF|XAG|XAU|XBA|XBB|XBC|XBD|XCD|XDR|XOF|XPD|XPF|XPT|XSU|XTS|XUA|XXX|YER|ZAR|ZMW|ZWL)$";
    public const string RegexValidacionPorcentaje = @"^[0-9]{0,3}(\.?[0-9]{0,6})$";
    public const string RegexTipoIdentificacion = @"^(11|12|13|21|22|31|41|42|50|91)$";
    public const string RegexNumeroDocumentoAlfanumerico = @"^[a-zA-Z0-9]{6,20}$";
    public const string RegexEstatusDian = @"^(0|2|66|89|90|99|100|109|200|201|202|203|204|500)$";
    public const string RegexEstatusAcuseRecibo = @"^(null|0|1|2|3)$";
    public const string RegexEstatusAcuseCorreoElectronico = @"^(1|2|3)$";
    public const string RegexEstatusTFHKA = @"^(200|201|99|407)$";
    public const string RegexTipoDE = @"^(01|02|03|04|91|92)$";
    public const long MaximoValorMontoDecimal = 999999999999999;
    public const string RegexMontoDecimal = @"^[0-9]{0,15}(\.[0-9]{0,6})?$";
    public const string RegexTipoAplication = @"^(1|2|3)$";
    public const string RegexPrefijoNumeroDocumento = @"^([a-zA-Z0-9]{0,20})$";
    public const string RegexNumeric0a8 = "^[0-9]{0,8}$";
    public const string RegexCodigoSucursal = @"^([a-zA-Z0-9äÄëËïÏöÖüÜáéíóúÁÉÍÓÚñÑ\-\s]{0,20})$";
    public const string RegexAmbiente = @"^(1|2)$";
    public const string RegexPrioridad = @"^(Normal|Alta)$";
    public const string RegexCantidadDecimales = @"^(0|1|2|3|4|5|6)$";
    public const string RegexEmail = @"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$";
    public const string RegexFormatoArchivo = @"^(csv|json)$";
    public const string RegexCodigoReporteProgramadoFacturacion = @"^(2|6|10)$";
    public const string TimeoutRedisCache = "No se pudo actualizar la caché. La operación de Inserción/Actualización/Eliminación del registro falló.Message: The operation timed out.. InnerException: ";

    public static bool EstaEnRangoEntero(string numero, int minimo, int maximo)
    {
      bool result = int.TryParse(numero, out int numeroEntero);
      return ((result) && (numeroEntero >= minimo) && (numeroEntero <= maximo));
    }
    public static bool EstaEnRangoEnteroLargo(string numero, long minimo, long maximo)
    {
      bool result = long.TryParse(numero, out long entero);
      return ((result) && (entero >= minimo) && (entero <= maximo));
    }

    public static bool EstaEnRangoDecimal(string numero, decimal minimo, decimal maximo)
    {
      bool result = decimal.TryParse(numero, out decimal numeroDecimal);
      return ((result) && (numeroDecimal >= minimo) && (numeroDecimal <= maximo));
    }

    public static readonly string[] Plataformas = { "DIAN", "TFHKA" };

    public static bool EsPlataformaValida(string plataforma)
    {
      return (Plataformas.Contains(plataforma));
    }

    public static readonly string[] TiposSesion = { "Iniciar", "Actualizar" };

    public static bool EsTipoSesionValido(string tipoSesion)
    {
      return (TiposSesion.Contains(tipoSesion));
    }

    public static string[]? ListaTipoDeConsultaPermitidos(IConfiguration configuracion)
    {
      return (configuracion
        .GetSection("ConsultaReferenciaDocumentoTipo")
        .GetChildren()
        .Select(c => c.Value).ToArray()!);
    }

    public static string[]? ListaCatalogosPermitidos(IConfiguration configuracion)
    {
      return (configuracion
        .GetSection("BaseDeDatos:SoloLectura:Catalogos")
        .GetChildren()
        .Select(c => c.Key).ToArray());
    }

    public static string[]? ListaValoresPermitidosFiltroTipoTotalDocumentos(IConfiguration configuracion)
    {
      return (configuracion
        .GetSection("ServiciosFacturacion:IndicadoresRest:ConsultarTotalDocumentos:ValoresPermitidosFiltroTipo")
        .GetChildren()
        .Select(c => c.Value).ToArray()!);
    }
  }
}
