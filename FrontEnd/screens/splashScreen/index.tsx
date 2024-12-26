import React from "react";
import { Text, Image, TouchableOpacity } from "react-native";
import * as SplashScreen from "expo-splash-screen";
import { LinearGradient } from "expo-linear-gradient";
import { styles } from "./styles";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { RootStackParamList } from "@/types/types";

type SplashScreenNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "Splash"
>;

const SplashScreenComponent = () => {
  const navigation = useNavigation<SplashScreenNavigationProp>();
  React.useEffect(() => {
    async function loadResourcesAndDataAsync() {
      try {
        await SplashScreen.hideAsync();
      } catch (e) {
        console.warn(e);
      } finally {
        await SplashScreen.hideAsync();
      }
    }

    loadResourcesAndDataAsync();
  }, []);

  return (
    <LinearGradient colors={["#FFE484", "#ffffff"]} style={styles.container}>
      <Text style={styles.tagline}>Play Together, Thrive Together!</Text>
      <Image
        source={{
          uri: "https://s3-alpha-sig.figma.com/img/a57f/2358/7f201ed585301973b901bcaafac36cf3?Expires=1728259200&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&Signature=NhxKsVxUY48dy6g8cHwviS~qvuV~cRLj06pqPxDrahzQYxwQ-5V4Jn4i4PaFWhmMhNB1QpRLFF~BjHvyQSEFcilmj1UxMC--8yJizzeLRQXBLWtO69Eq2FQ6wne4zxKRkgArl2M2XOilZ~17S5YVsHQ~DX-E2NQuooPlCJmulNqw7lJcZHNkXPSBs3GB6GFt8BrKA-r64wFA4Gc8qIial2OQ~pk-h~XR1ZgZrw5s2aWNubXUI9ER5-80FhAmxSxFRls8W9Rs5cgpnXe~ZgDLWytEpNgOiuekiFWzwYUuYgRK7Ox6w6pyQ00DQ40FuFeSH6scAMoMZEHCmCseU-LrpQ__",
        }}
        style={styles.logo}
      />
      <TouchableOpacity
        style={styles.button}
        onPress={() => {
          navigation.navigate("SignUp");
        }}
      >
        <Text style={styles.buttonText}>Sign Up</Text>
      </TouchableOpacity>
      <TouchableOpacity
        style={styles.loginButton}
        onPress={() => {
          navigation.navigate("Login");
        }}
      >
        <Text style={styles.loginButtonText}>
          Already have an account? <Text style={styles.loginText}>Login</Text>
        </Text>
      </TouchableOpacity>
    </LinearGradient>
  );
};

export default SplashScreenComponent;