import React, { useState } from "react";
import { View, Text, TouchableOpacity, Platform } from "react-native";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { RootStackParamList } from "@/types/types";
import { styles } from "./styles";
import {
  CodeField,
  Cursor,
  useBlurOnFulfill,
  useClearByFocusCell,
} from "react-native-confirmation-code-field";

type VerificationScreenNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "Verification"
>;

const CELL_COUNT = 4;

const VerificationScreen = () => {
  const [value, setValue] = useState("");
  const navigation = useNavigation<VerificationScreenNavigationProp>();

  const ref = useBlurOnFulfill({ value, cellCount: CELL_COUNT });
  const [props, getCellOnLayoutHandler] = useClearByFocusCell({
    value,
    setValue,
  });

  const handleVerify = () => {
    if (value.length === CELL_COUNT) {
      navigation.navigate("NewPassword");
      console.log("Verification code:", value);
    } else {
      console.log("Incomplete verification code");
    }
  };

  const handleResend = () => {
    console.log("Resend verification code");
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Verification</Text>
      <Text style={styles.subtitle}>Enter Verification Code</Text>

      <CodeField
        ref={ref}
        {...props}
        value={value}
        onChangeText={setValue}
        cellCount={CELL_COUNT}
        rootStyle={styles.codeFieldRoot}
        keyboardType="number-pad"
        textContentType="oneTimeCode"
        autoComplete={
          Platform.select({
            android: "sms-otp",
            ios: "one-time-code",
            default: undefined,
          }) as "sms-otp" | "one-time-code" | undefined
        }
        renderCell={({ index, symbol, isFocused }) => (
          <Text
            key={index}
            style={[styles.cell, isFocused && styles.focusCell]}
            onLayout={getCellOnLayoutHandler(index)}
          >
            {symbol || (isFocused ? <Cursor /> : null)}
          </Text>
        )}
      />

      <TouchableOpacity style={styles.resendButton} onPress={handleResend}>
        <Text style={styles.simpleText}>
          You did not receive a code.{" "}
          <Text style={styles.resendButtonText}>Resend</Text>
        </Text>
      </TouchableOpacity>

      <TouchableOpacity style={styles.verifyButton} onPress={handleVerify}>
        <Text style={styles.verifyButtonText}>Send</Text>
      </TouchableOpacity>

      {/* Social buttons and other components */}
      <View style={styles.socialContainer}>
        <Text style={styles.orText}>or</Text>
        <View style={styles.socialButtons}>
          <TouchableOpacity style={styles.socialButton}>
            <Text style={styles.socialButtonText}>Google</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.socialButton}>
            <Text style={styles.socialButtonText}>Facebook</Text>
          </TouchableOpacity>
        </View>
      </View>

      <Text style={styles.preSignUpText}>Don't have an account?</Text>
      <TouchableOpacity
        onPress={() => navigation.navigate("SignUp")}
        style={styles.signUpButton}
      >
        <Text style={styles.signUpButtonText}>Sign up</Text>
      </TouchableOpacity>
    </View>
  );
};

export default VerificationScreen;