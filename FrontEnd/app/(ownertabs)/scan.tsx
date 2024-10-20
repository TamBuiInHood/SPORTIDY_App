import React, { useState, useEffect } from 'react';
import { Text, View, StyleSheet, Button, Alert } from 'react-native';
import { Camera } from 'expo-camera';
import { BarCodeScanner } from 'expo-barcode-scanner';
import { useNavigation } from '@react-navigation/native'; // Import useNavigation
import axios from 'axios';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
type SignUpScreenNavigationProp = NativeStackNavigationProp<
  RootStackParamList, 
  "SignUp"
>;

const ScanQRCodeScreen = () => {
  const navigation = useNavigation<SignUpScreenNavigationProp>(); // Initialize navigation
  const [hasPermission, setHasPermission] = useState<boolean | null>(null);
  const [scanned, setScanned] = useState<boolean>(false);

  // Request camera permissions
  useEffect(() => {
    const getCameraPermissions = async () => {
      const { status } = await Camera.requestCameraPermissionsAsync();
      setHasPermission(status === 'granted');
    };

    getCameraPermissions();
  }, []);

  // Handle QR code scanning
  const handleBarCodeScanned = async ({ type, data }: { type: string; data: string }) => {
    setScanned(true);
    try {
      const parsedData = JSON.parse(data); // Parse the JSON data from QR code
      navigation.navigate('QRCode', { bookingDetails: parsedData }); // Navigate to BookingDetails screen with parsed data
    } catch (error) {
      Alert.alert('Error','QR Code data is invalid or incomplete.');
    }
  };

  // Show loading while camera permissions are being requested
  if (hasPermission === null) {
    return <Text>Requesting for camera permission...</Text>;
  }

  // Show a message if camera permission is denied
  if (hasPermission === false) {
    return <Text>No access to camera</Text>;
  }

  return (
    <View style={styles.container}>
      <View style={styles.cameraContainer}>
        <BarCodeScanner
          onBarCodeScanned={scanned ? undefined : handleBarCodeScanned}
          style={StyleSheet.absoluteFillObject}
        />
      </View>
      {scanned && (
        <Button title={'Tap to Scan Again'} onPress={() => setScanned(false)} />
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  cameraContainer: {
    width: '100%',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default ScanQRCodeScreen;
