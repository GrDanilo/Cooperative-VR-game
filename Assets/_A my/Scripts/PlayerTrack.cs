using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class PlayerTrack : MonoBehaviour
{
    [Tooltip("Full path to the CSV file (e.g. C:/Data/positions.csv)")]
    public string filePath = "positions.csv";
    
    private StreamWriter _writer;
    private float _timer;

    void Start()
    {
        // Create directory if it doesn't exist
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize CSV file with headers
        _writer = new StreamWriter(filePath, false);
        _writer.WriteLine("Секунда,координата x,координата y,координата z");
        _writer.Flush();

        // Start repeating log every 10 seconds
        InvokeRepeating(nameof(LogPosition), 10f, 10f);
    }

    void LogPosition()
    {
        float currentTime = Time.time;
        Vector3 position = transform.position;
        
        // Format numbers with invariant culture to ensure proper decimal formatting
        string line = string.Format(CultureInfo.InvariantCulture,
            "{0:F2},{1:F2},{2:F2},{3:F2}",
            currentTime,
            position.x,
            position.y,
            position.z);

        _writer.WriteLine(line);
        _writer.Flush();
    }

    void OnDestroy()
    {
        if (_writer != null)
        {
            _writer.Close();
            _writer = null;
        }
    }
}
