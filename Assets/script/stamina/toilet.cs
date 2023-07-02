using UnityEngine;
using UnityEngine.UI;

public class toilet : MonoBehaviour
{
    public float maxStamina = 100f; // Nilai maksimum stamina
    public Slider usageWheel; // Slider untuk menampilkan penggunaan stamina

    private float decrementRate; // Pengurangan stamina per detik
    private float timeCounter = 0f; // Penghitung waktu

    private Color originalColor = new Color(0.3804f, 0.9255f, 0.1176f); // Warna asli slider (61EC1E)
    private Color redColor = Color.red; // Warna merah

    private float currentStamina; // Variabel stamina saat ini

    void Start()
    {
        // Mengambil nilai stamina terakhir dari GameManager
        if (GameManager.Instance != null)
        {
            currentStamina = GameManager.Instance.currentStamina;
        }
        else
        {
            currentStamina = maxStamina;
        }

        decrementRate = maxStamina / 350f; // Menghitung pengurangan stamina per detik (60 detik = 1 menit)
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        currentStamina = Mathf.Max(currentStamina - (decrementRate * Time.deltaTime), 0f); // Mengurangi stamina dengan pengurangan per detik

        Debug.Log("Current Stamina: " + currentStamina);

        if (usageWheel != null)
        {
            usageWheel.value = (currentStamina / maxStamina) + 0f;

            if (Mathf.FloorToInt(timeCounter) >= 80) // Ketika angka berada di detik 10 atau lebih
            {
                ChangeSliderColor(redColor); // Ubah warna slider menjadi merah
            }
            else
            {
                ChangeSliderColor(originalColor); // Kembalikan warna slider ke warna asli
            }
        }

        if (currentStamina <= 0)
        {
            // Lakukan tindakan ketika stamina habis
        }
    }

    private void ChangeSliderColor(Color color)
    {
        if (usageWheel != null)
        {
            var fillRect = usageWheel.fillRect;
            if (fillRect != null)
            {
                var fillImage = fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    fillImage.color = color;
                }
            }
        }
    }

    private void OnDestroy()
    {
        // Menyimpan nilai stamina ke GameManager saat objek dihancurkan
        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentStamina = currentStamina;
        }
    }
}
