using UnityEngine;

public class GPSEncoderUtil {

    public static bool OriginInitComplete { get; set; }

    public static bool IsReadyToInitOrigin() {
        return Input.location.status == LocationServiceStatus.Running ||
                    Application.isEditor;

    }

}
