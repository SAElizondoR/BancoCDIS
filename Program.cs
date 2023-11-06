using System.Collections.Specialized;

List<ushort> retiros = new();
ushort opcion;
int totalRetiros = 0;

do
{
    string lineas = new('-', 24);
    Console.WriteLine(lineas + " Banco CDIS " + lineas
        + "\n1. Ingresar la cantidad de retiros hechos por los usuarios."
        + "\n2. Revisar la cantidad entregada de billetes y monedas.\n");
    opcion = ingresarNumero("Ingrese la opción:", 1, 2);

    switch (opcion)
    {
        case 1:
            ushort numRetiros = ingresarNumero("¿Cuántos retiros se hicieron (máximo 10)?", 0, 10);

            if (numRetiros > 10)
            {
                Console.WriteLine("El número de retiros supera el máximo de 10.");
                break;
            }

            for (byte i = 0; i < numRetiros; i++)
            {
                ushort cantidad = ingresarNumero($"Ingresa la cantidad del retiro {i + 1}:", 0, 50000);
                retiros.Add(cantidad);
            }
            totalRetiros += numRetiros;
            break;

        case 2:
            for (int i = 0; i < totalRetiros; i++)
            {
                int cantidad = retiros[i];

                OrderedDictionary contador = new() {
                    {500, 0}, {200, 0}, {100, 0}, {50, 0}, {20, 0},
                    {10, 0}, {5, 0}, {1, 0}
                };
                ushort billetes = 0, monedas = 0;

                foreach (Int32 denominacion in contador.Keys)
                {
                    while (cantidad >= denominacion)
                    {
                        if (denominacion >= 20)
                            billetes++;
                        else
                            monedas++;

                        cantidad -= denominacion;
                    }
                }

                Console.WriteLine($"Retiro {i + 1}:\n"
                    + $"Billetes entregados: {billetes}\n"
                    + $"Monedas entregadas: {monedas}\n");
            }

            Console.Write("Presiona 'enter' para continuar ... ");
            Console.ReadLine();

            break;
        
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
    Console.WriteLine();
} while (opcion == 1 || opcion == 2);

static ushort ingresarNumero(string mensaje, ushort valorMinimo, ushort valorMaximo)
{
    ushort valor = 0;
    bool entradaValida = false;

    do
    {
        Console.WriteLine(mensaje);
        if (ushort.TryParse(Console.ReadLine(), out valor))
        {
            if (valor >= valorMinimo && valor <= valorMaximo)
            {
                entradaValida = true;
            }
            else
            {
                Console.WriteLine($"El valor debe estar entre {valorMinimo} y {valorMaximo}.\n");
            }
        }
        else
        {
            Console.WriteLine("Entrada no válida; debe ser un número entero positivo menor que 65535.\n");
        }
    } while (!entradaValida);

    return valor;
}
