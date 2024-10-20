import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Image,
  Alert,
} from "react-native";
import { LinearGradient } from "expo-linear-gradient";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { RootStackParamList } from "../../types/types";
import { styles } from "./style";
import Divider from "@/components/Divider";
import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios"; // Import axios for API requests
import frameImage from "../../assets/images/frame.png";
import { decode as base64Decode } from "base-64";

type LoginScreenNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "Login"
>;

const LoginScreen = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false); // Track loading state
  const navigation = useNavigation<LoginScreenNavigationProp>();
  const decodeJWT = (token: string) => {
    try {
      const base64Url = token.split(".")[1]; // The second part of the JWT
      const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/"); // Adjust base64 for URL encoding
  
      const jsonPayload = base64Decode(base64);
      return JSON.parse(jsonPayload); // Convert the string to an object
    } catch (error) {
      console.error("Failed to decode JWT:", error);
      return null; // Handle error if decoding fails
    }
  };
  const handleLogin = async () => {
    setLoading(true);
    try {
      const response = await axios.post(
        "https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/authentication/login",
        {
          email: email,
          password: password,
        }
      );

      const { accessToken } = response.data;

      if (accessToken) {
        const decodedToken = decodeJWT(accessToken);
        console.log("Decoded token:", decodedToken); 

        const roleId = decodedToken.role;
        const userId = decodedToken.UserId;  

        if (roleId && userId) {
          await AsyncStorage.setItem("userRole", roleId);  // Store userRole
          await AsyncStorage.setItem("userId", userId);  // Store UserId
          console.log("User role:", roleId);
          console.log("User ID:", userId);

          // Navigate based on user role
          if (roleId === 'SPORTOWNER') {
            navigation.navigate("(ownertabs)", { params: { screen: "home" } });
          } else if (roleId === 'PLAYER') {
            navigation.navigate("(tabs)", { params: { screen: "index" } });
          }
        } else {
          Alert.alert("Error", "User information not found in token.");
        }
      } else {
        Alert.alert("Error", "Login failed. No token received.");
      }
    } catch (error) {
      console.error("Login failed:", error);
      Alert.alert("Error", "Something went wrong, please try again.");
    } finally {
      setLoading(false);
    }
  };

  const handleGoogleLogin = () => {
    console.log("Login with Google");
  };

  return (
    <View style={styles.container}>
      <Image source={frameImage} style={styles.logo} />
      <LinearGradient colors={["#F9BC2C", "#F74c06"]} style={styles.form}>
        <View style={styles.inputContainer}>
          <Text style={styles.textInput}>Email</Text>
          <TextInput
            style={styles.input}
            value={email}
            onChangeText={setEmail}
            keyboardType="email-address"
            autoCapitalize="none"
          />
          <Text style={styles.textInput}>Password</Text>
          <TextInput
            style={styles.input}
            value={password}
            onChangeText={setPassword}
            secureTextEntry
          />
          <TouchableOpacity
            onPress={() => {
              navigation.navigate("ForgotPassword");
            }}
            style={styles.forgotPassword}
          >
            <Text style={styles.forgotPasswordText}>Forgot Password?</Text>
          </TouchableOpacity>
        </View>

        <TouchableOpacity
          style={styles.loginButton}
          onPress={handleLogin}
          disabled={loading} // Disable button while loading
        >
          <Text style={styles.loginButtonText}>
            {loading ? "Logging in..." : "Login"}
          </Text>
        </TouchableOpacity>

        <Divider text="Or Sign In with" />

        <TouchableOpacity style={styles.googleButton} onPress={handleGoogleLogin}>
          <Image
            source={{
              uri: "https://cdn1.iconfinder.com/data/icons/google-s-logo/150/Google_Icons-09-512.png",
            }}
            style={styles.googleIcon}
          />
          <Text style={styles.googleButtonText}>Google</Text>
        </TouchableOpacity>

        <TouchableOpacity onPress={() => navigation.navigate("SignUp")}>
          <Text style={styles.simpleText}>
            Don't have an account?{" "}
            <Text style={styles.signUpLink}>Sign Up</Text>
          </Text>
        </TouchableOpacity>
      </LinearGradient>
    </View>
  );
};

export default LoginScreen;
