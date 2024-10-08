import * as Notifications from "expo-notifications";
import { useState, useEffect, useRef } from "react";
import { Alert, Platform } from "react-native";
import useGetDeviceToken from "./useGetDeviceToken";

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
  const getDeviceToken = useGetDeviceToken();

  Notifications.setNotificationHandler({
    handleNotification: async () => {
      return {
        shouldPlaySound: false,
        shouldSetBadge: false,
        shouldShowAlert: true,
      };
    },
  });

  useEffect(() => {
    setExpoPushToken(getDeviceToken);

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
      to: "ExponentPushToken[_kpyCIBYF-14SxPqQJPkO3]",
      sound: "default",
      title: title,
      body: body,
      data: { someData: "goes here" },
    };
    console.log("Test send: ", expoPushToken);
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
