import { DarkTheme, DefaultTheme, NavigationContainer, ThemeProvider } from '@react-navigation/native';
import { useFonts } from 'expo-font';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import * as SplashScreen from 'expo-splash-screen';
import { useEffect } from 'react';
import 'react-native-reanimated';
import { useColorScheme } from '@/hooks/useColorScheme';
import { GestureHandlerRootView } from 'react-native-gesture-handler';
import DetailBookingPage from '@/screens/booking/bookingDetail';
import LoginScreen from '@/screens/login';
import SignUpScreen from '@/screens/signUp';
import TabLayout from './(tabs)/_layout';
import NewPasswordScreen from '@/screens/newPassword';
import ForgotPasswordScreen from '@/screens/forgotPassword';
import VerificationScreen from '@/screens/verification';
import SplashScreenComponent from '@/screens/splashScreen';
import EventDetailScreen from '@/screens/home/_layout';
import { RootStackParamList } from '@/types/types';
import BookingInformationPage from '@/screens/booking/bookingInfoPage';
import PaymentSuccessPage from '@/screens/booking/paymentBooking';
import YourMeeting from './(tabs)/club';
import MyHistory from '@/screens/viewBookingHistory/bookingHistory';
import BookingDetail from '@/screens/viewBookingHistory/historyDetail';
import FeedbackPage from '@/screens/feedback/feedback';
import OwnerTabLayout from './(ownertabs)/_layout';
import PlayFieldDetailCard from '@/screens/playFieldDetail';
import PlayfieldList from '@/screens/playField';
import CreatePlayfield from '@/screens/createPlayField';
import UpdatePlayField from '@/screens/updatePlayField';

SplashScreen.preventAutoHideAsync();
const Stack = createNativeStackNavigator<RootStackParamList>();

export default function RootLayout() {
  const colorScheme = useColorScheme();
  const [loaded] = useFonts({
    SpaceMono: require('../assets/fonts/SpaceMono-Regular.ttf'),
  });

  useEffect(() => {
    if (loaded) {
      SplashScreen.hideAsync();
    }
  }, [loaded]);

  if (!loaded) {
    return null;
  }

  return (
    <GestureHandlerRootView style={{ flex: 1 }}>
      <NavigationContainer theme={colorScheme === 'dark' ? DarkTheme : DefaultTheme} independent={true} >
        <Stack.Navigator initialRouteName="Splash" screenOptions={{ headerShown: false }}>
          <Stack.Screen name="Splash" component={SplashScreenComponent} />
          <Stack.Screen name="Login" component={LoginScreen} />
          <Stack.Screen name="NewPassword" component={NewPasswordScreen} />
          <Stack.Screen name="ForgotPassword" component={ForgotPasswordScreen} />
          <Stack.Screen name="Verification" component={VerificationScreen} />
          <Stack.Screen name="SignUp" component={SignUpScreen} />
          <Stack.Screen name="(tabs)" component={TabLayout} options={{ headerShown: false }} />
          <Stack.Screen name="EventDetail" component={EventDetailScreen} />
          <Stack.Screen name="DetailBookingPage" component={DetailBookingPage} />
          <Stack.Screen name="BookingInformationPage" component={BookingInformationPage} />
          <Stack.Screen name="PaymentBooking" component={PaymentSuccessPage} />
          <Stack.Screen name="YourMeeting" component={YourMeeting} />
          <Stack.Screen name="MyHistory" component={MyHistory} />
          <Stack.Screen name="HistoryDetail" component={BookingDetail} />
          <Stack.Screen name="FeedBack" component={FeedbackPage} />
          <Stack.Screen name="(ownertabs)" component={OwnerTabLayout} options={{ headerShown: false }} />
          <Stack.Screen name= "PlayfieldDetailCard" component={PlayFieldDetailCard}/>
          <Stack.Screen name= "PlayFieldList" component={PlayfieldList}/>
          <Stack.Screen name= "CreatePlayfield" component={CreatePlayfield}/>
          <Stack.Screen name= "UpdatePlayfield" component={UpdatePlayField}/>

        </Stack.Navigator>
      </NavigationContainer>
    </GestureHandlerRootView>
  );
}
