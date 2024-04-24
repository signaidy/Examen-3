using System;
using System.Collections.Generic;

// Clase que representa una señal de datos
public class Signal {
    public string Data { get; set; }

    public Signal(string data) {
        Data = data;
    }
}

// Clase que simula un generador de señales
public class SignalGenerator {
    public List<Signal> GenerateSignals(int numSignals) {
        List<Signal> signals = new List<Signal>();
        for (int i = 0; i < numSignals; i++) {
            signals.Add(new Signal($"Signal {i+1}"));
        }
        return signals;
    }
}

// Clase que representa un modulador óptico
public class OpticalModulator {
    public string ModulateSignal(Signal signal, double wavelength) {
        // Aquí simulamos la modulación óptica
        return $"Signal '{signal.Data}' modulated at wavelength {wavelength}";
    }
}

// Clase que representa un multiplexor óptico
public class OpticalMultiplexer {
    public string MultiplexSignals(List<string> modulatedSignals) {
        // Aquí simulamos la multiplexación óptica
        return string.Join(", ", modulatedSignals);
    }
}

// Clase que representa un demultiplexor óptico
public class OpticalDemultiplexer {
    public List<string> DemultiplexSignal(string multiplexedSignal) {
        // Aquí simulamos la demultiplexación óptica
        return new List<string>(multiplexedSignal.Split(", "));
    }
}

// Clase que representa un demodulador óptico
public class OpticalDemodulator {
    public string DemodulateSignal(string modulatedSignal) {
        // Aquí simulamos la demodulación óptica
        return modulatedSignal.Replace(" modulated at", " demodulated at");
    }
}

// Clase principal que simula el proceso completo de transmisión DWDM
class Program {
    static void Main(string[] args) {
        // Generar señales de datos
        SignalGenerator generator = new SignalGenerator();
        List<Signal> signals = generator.GenerateSignals(3);

        // Modular señales en diferentes longitudes de onda
        OpticalModulator modulator = new OpticalModulator();
        Dictionary<Signal, double> modulatedSignals = new Dictionary<Signal, double>();
        double currentWavelength = 1550.0; // Longitud de onda inicial
        foreach (Signal signal in signals) {
            modulatedSignals.Add(signal, currentWavelength);
            currentWavelength += 0.8; // Incremento arbitrario para las siguientes señales
        }

        // Multiplexar señales moduladas
        OpticalMultiplexer multiplexer = new OpticalMultiplexer();
        List<string> modulatedSignalStrings = new List<string>();
        foreach (var kvp in modulatedSignals) {
            string modulatedSignal = modulator.ModulateSignal(kvp.Key, kvp.Value);
            modulatedSignalStrings.Add(modulatedSignal);
        }
        string multiplexedSignal = multiplexer.MultiplexSignals(modulatedSignalStrings);

        // Transmisión
        Console.WriteLine($"Transmitting multiplexed signal: {multiplexedSignal}");

        // Recepción y desmultiplexación
        OpticalDemultiplexer demultiplexer = new OpticalDemultiplexer();
        List<string> demultiplexedSignals = demultiplexer.DemultiplexSignal(multiplexedSignal);

        // Demodulación y procesamiento de señales individuales
        OpticalDemodulator demodulator = new OpticalDemodulator();
        foreach (string demultiplexedSignal in demultiplexedSignals) {
            string demodulatedSignal = demodulator.DemodulateSignal(demultiplexedSignal);
            Console.WriteLine($"Received and demodulated signal: {demodulatedSignal}");
        }
    }
}
