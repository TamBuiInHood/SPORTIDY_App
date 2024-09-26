import { useState, useEffect } from "react";
import * as Notifications from "expo-notifications";
import { Alert, Platform } from "react-native";

const useGetDeviceToken = () => {
  const [pushToken, setPushToken] = useState<string | null>(null);

  useEffect(() => {
    const registerForPushNotifications = async () => {
      try {
        // Kiểm tra trạng thái cấp quyền
        const { status: existingStatus } =
          await Notifications.getPermissionsAsync();
        let finalStatus = existingStatus;

        // Nếu chưa có quyền, yêu cầu cấp quyền
        if (existingStatus !== "granted") {
          const { status } = await Notifications.requestPermissionsAsync();
          finalStatus = status;
        }

        // Nếu quyền vẫn không được cấp, hiển thị thông báo lỗi
        if (finalStatus !== "granted") {
          Alert.alert("Failed to get push token for push notification!");
          return;
        }

        // Lấy token cho thông báo đẩy từ Expo
        const token = (
          await Notifications.getExpoPushTokenAsync({
            projectId: "0b7996f2-0e57-4bd6-b44c-f8c7af930995", // Thay projectId của bạn
          })
        ).data;

        // In token ra console và lưu vào state
        console.log("Expo Push Token:", token);
        setPushToken(token);
      } catch (error) {
        console.error("Error getting push token:", error);
      }
    };

    // Chỉ đăng ký thông báo đẩy trên thiết bị vật lý, không phải trên trình giả lập Android
    if (Platform.OS === "android") {
      Notifications.setNotificationChannelAsync("default", {
        name: "default",
        importance: Notifications.AndroidImportance.MAX,
      });
    }

    registerForPushNotifications();
  }, []);

  return pushToken;
};

export default useGetDeviceToken;
