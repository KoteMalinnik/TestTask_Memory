using UnityEngine;

//Источник: https://gist.github.com/Glavak/ada99b57023db3c941c5caebe42a70c5

[RequireComponent(typeof(Camera))]
public class CameraFovController: MonoBehaviour
{
    #region Serialize Fields

    //разрешение, к которому будет произведена привязка
    [SerializeField] private Vector2 DefaultResolution = new Vector2(480, 800);

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Log.Message("Расчет вертикального FOV камеры");

        Camera camera = GetComponent<Camera>();

        float defaultAspectRatio = DefaultResolution.x / DefaultResolution.y;

        float horizontalFov = camera.fieldOfView; //у камеры должен стоять FOV Axis = Horizontal
        float verticalFov = GetVerticalFov(horizontalFov, 1 / defaultAspectRatio); //вертикальный fov стандартного соотношения сторон
        float constWidthFov = GetVerticalFov(verticalFov, camera.aspect); //вертикальный fov для текущего соотношения сторон

        camera.fieldOfView = constWidthFov;

        Log.Message($"Стандартное соотношение сторон: {defaultAspectRatio}. Вертикальный FOV: {verticalFov}");
        Log.Message($"Текущее соотношение сторон: {camera.aspect}. Рассчетный вертикальный FOV: {constWidthFov}");

        Destroy(this);
    }

    #endregion

    #region Private Methods

    //если в fieldOfView перередать вертикальный fov, а aspectRatio передать aspectRatio,
    //то получится вертикальный fov для соотношения сторон aspectRatio
    private float GetVerticalFov(float fieldOfView, float aspectRatio)
    {
        fieldOfView *= Mathf.Deg2Rad; //перевод в радианы
        
        float verticalFov = 2 * Mathf.Atan(Mathf.Tan(fieldOfView / 2) / aspectRatio);

        verticalFov *= Mathf.Rad2Deg; //перевод в градусы

        return verticalFov;
    }

    #endregion
}