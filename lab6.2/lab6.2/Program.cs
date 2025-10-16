using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
 
public interface IObserver
{
    void Update(float temperature);
}
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}
public class WeatherStation : ISubject
{
    private List<IObserver> _observers;
    private float _temperature;

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }
    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }
    public void Notify()
    {
        foreach (IObserver observer in _observers)
        {
            observer.Update(_temperature);
        }
    }
    public void SetTemperature(float updatedTemperature)
    {
        Console.WriteLine($"Temperature changed: {updatedTemperature}");
        _temperature = updatedTemperature;
        Notify();
    }
}
public class WeatherDisplay : IObserver
{
    private string _name;
    public WeatherDisplay(string name) {
        _name = name; }
    public void Update(float temperature)
    {
        Console.WriteLine($"{_name} get notify: temperature is changed to {temperature}");
    }

}
class Program
{
    static void Main(string[] args)
    {
        WeatherStation weatherStation = new WeatherStation();   
        WeatherDisplay display = new WeatherDisplay("Weather news site");
        weatherStation.Attach(display);
        weatherStation.SetTemperature(25.0f);
        weatherStation.SetTemperature(23.0f);
    }
}