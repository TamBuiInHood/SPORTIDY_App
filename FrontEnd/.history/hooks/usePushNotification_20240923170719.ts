import * as Notifications from "expo-notifications";
import { useState, useEffect, useRef } from "react";
import { Platform } from "react-native";

interface NotificationResponse {
  // Thêm các thuộc tính cần thiết từ phản hồi thông báo
  // Bạn có thể điều chỉnh theo yêu cầu của ứng dụng
}

interface UsePushNotification {
  sendPushNotification: (title: string, body: string) => Promise<void>;
  expoPushToken: string | undefined;
  notification: Notifications.Notification | boolean;
  showLocalNotification: (title: string, body: string) => Promise<void>;
}

export default function usePushNotification(): UsePushNotification {
  const [expoPushToken, setExpoPushToken] = useState<string | undefined>("");
  const [notification, setNotification] = useState<
    Notifications.Notification | boolean
  >(false);
  const notificationListener = useRef<Notifications.Subscription | null>(null);
  const responseListener = useRef<Notifications.Subscription | null>(null);

  useEffect(() => {
    // Lấy expo push token
    registerForPushNotificationsAsync().then((token) =>
      setExpoPushToken(token)
    );

    // Lắng nghe khi nhận thông báo
    notificationListener.current =
      Notifications.addNotificationReceivedListener((notification) => {
        setNotification(notification);
      });

    // Lắng nghe khi người dùng nhấn vào thông báo
    responseListener.current =
      Notifications.addNotificationResponseReceivedListener(
        (response: NotificationResponse) => {
          console.log(response);
          //   const { screen } = response.request.content.data;
          //     // Giả sử bạn đang sử dụng React Navigation
          //     if (screen) {
          //         navigation.navigate(screen); // Chuyển tới màn hình mong muốn
          //     }
        }
      );

    return () => {
      if (notificationListener.current) {
        Notifications.removeNotificationSubscription(
          notificationListener.current
        );
      }
      if (responseListener.current) {
        Notifications.removeNotificationSubscription(responseListener.current);
      }
    };
  }, []);

  // Hàm gửi thông báo
  const sendPushNotification = async (
    title: string,
    body: string
  ): Promise<void> => {
    const message = {
      to: expoPushToken,
      sound: "default",
      title: title,
      body: body,
      data: { someData: "goes here" },
    };

    await fetch("https://exp.host/--/api/v2/push/send", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(message),
    });
  };
  // Hàm hiển thị thông báo cục bộ ngay lập tức
  const showLocalNotification = async (title: string, body: string) => {
    await Notifications.scheduleNotificationAsync({
      content: {
        title: title,
        body: body,
        sound: true,
      },
      trigger: null, // trigger: null để hiển thị ngay lập tức
    });
  };

  return {
    sendPushNotification,
    expoPushToken,
    notification,
    showLocalNotification,
  };
}

// Hàm đăng ký lấy token cho Expo Push Notifications
async function registerForPushNotificationsAsync(): Promise<
  string | undefined
> {
  let token: string | undefined;
  if (Platform.OS === "android") {
    await Notifications.setNotificationChannelAsync("default", {
      name: "default",
      importance: Notifications.AndroidImportance.MAX,
      vibrationPattern: [0, 250, 250, 250],
      lightColor: "#FF231F7C",
    });
  }

  const { status: existingStatus } = await Notifications.getPermissionsAsync();
  let finalStatus = existingStatus;
  if (existingStatus !== "granted") {
    const { status } = await Notifications.requestPermissionsAsync();
    finalStatus = status;
  }
  if (finalStatus !== "granted") {
    alert("Failed to get push token for push notification!");
    return;
  }
  token = (
    await Notifications.getExpoPushTokenAsync({
      projectId: "0b7996f2-0e57-4bd6-b44c-f8c7af930995",
    })
  ).data;
  console.log("Expo Push Token:", token);
  return token;
}
