import { useState, useEffect } from "react";
import { Platform, Alert } from "react-native";
import * as Notifications from "expo-notifications";

const useGetDeviceToken = (): string | undefined => {
  const [token, setToken] = useState<string | undefined>(undefined);

  useEffect(() => {
    const registerForPushNotificationsAsync = async (): Promise<void> => {
      try {
        let expoPushToken: string | undefined;

        // Cấu hình channel cho Android
        if (Platform.OS === "android") {
          await Notifications.setNotificationChannelAsync("default", {
            name: "default",
            importance: Notifications.AndroidImportance.DEFAULT,
            vibrationPattern: [0, 250, 250, 250],
            lightColor: "#FF231F7C",
          });
        }

        // Kiểm tra quyền thông báo
        const { status: existingStatus } =
          await Notifications.getPermissionsAsync();
        let finalStatus = existingStatus;

        // Nếu chưa được cấp quyền, yêu cầu quyền
        if (existingStatus !== "granted") {
          const { status } = await Notifications.requestPermissionsAsync();
          finalStatus = status;
        }

        // Nếu quyền vẫn chưa được cấp, hiển thị thông báo lỗi
        if (finalStatus !== "granted") {
          Alert.alert("Failed to get push token for push notification!");
          return;
        }

        // Lấy token từ Expo
        expoPushToken = (
          await Notifications.getExpoPushTokenAsync({
            projectId: "0b7996f2-0e57-4bd6-b44c-f8c7af930995", // Thay bằng projectId của bạn
          })
        ).data;

        console.log("Expo Push Token:", expoPushToken);
        setToken(expoPushToken);
      } catch (error) {
        console.error("Error getting push token:", error);
      }
    };

    registerForPushNotificationsAsync();
  }, []);

  return token;
};

export default useGetDeviceToken;
