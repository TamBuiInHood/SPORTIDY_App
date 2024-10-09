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
import frameImage from '../../assets/images/frame.png'

type LoginScreenNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "Login"
>;

const LoginScreen = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigation = useNavigation<LoginScreenNavigationProp>();
  const users = [
    { email: "user@gmail.com", password: "123456", role: "user" },
    { email: "owner@gmail.com", password: "123456", role: "owner" },
  ];
  const handleLogin = async () => {
    const foundUser = users.find(
      (user) => user.email === email && user.password === password
    );

    if (foundUser) {
      try {
        await AsyncStorage.setItem("userEmail", email);
        await AsyncStorage.setItem("userRole", foundUser.role);

        console.log("User role:", foundUser.role);

        if (foundUser.role === "user") {
          navigation.navigate("(tabs)", { params: { screen: "index" } });
        } else if (foundUser.role === "owner") {
          navigation.navigate("(ownertabs)", { params: { screen: "home" } });
        }
      } catch (error) {
        console.error("Failed to save data", error);
      }
    } else {
      Alert.alert("Error", "Invalid email or password.");
    }
  };

  const handleGoogleLogin = () => {
    // TODO: Implement Google login logic here
    console.log("Login with Google");
  };

  return (
    <View style={styles.container}>
      <Image
        source={frameImage}
        style={styles.logo}
      />
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

        <TouchableOpacity style={styles.loginButton} onPress={handleLogin}>
          <Text style={styles.loginButtonText}>Login</Text>
        </TouchableOpacity>

        <Divider text="Or Sign In with" />

        <TouchableOpacity
          style={styles.googleButton}
          onPress={handleGoogleLogin}
        >
          <Image
            source={{ uri: 'https://cdn1.iconfinder.com/data/icons/google-s-logo/150/Google_Icons-09-512.png' }}
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