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
          {/* <Stack.Screen name="(routes)/bookingDetail" component={DetailBookingPage} /> */}

        </Stack.Navigator>
      </NavigationContainer>
    </GestureHandlerRootView>
  );
}
