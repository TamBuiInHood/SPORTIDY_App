import React, { useState, useEffect } from 'react';
import { Text, View, StyleSheet, Button, Alert } from 'react-native';
import { Camera } from 'expo-camera';

const ScanQRCodeScreen = () => {
  const [hasPermission, setHasPermission] = useState(null);
  const [scanned, setScanned] = useState(false);
  const [bookingDetails, setBookingDetails] = useState(null);

  // Yêu cầu quyền truy cập camera
  useEffect(() => {
    const getCameraPermissions = async () => {
      const { status } = await Camera.requestCameraPermissionsAsync();
      setHasPermission(status === 'granted');
    };

    getCameraPermissions();
  }, []);

  // Xử lý khi quét mã QR
  const handleBarCodeScanned = ({ type, data }) => {
    setScanned(true);
    try {
      const parsedData = JSON.parse(data); // Giải mã chuỗi JSON từ mã QR
      setBookingDetails(parsedData); // Lưu chi tiết đặt chỗ
    } catch (error) {
      Alert.alert('Error', 'QR Code data is invalid.');
    }
  };

  // Hiển thị thông báo khi chưa có quyền truy cập camera
  if (hasPermission === null) {
    return <Text>Requesting for camera permission...</Text>;
  }
  
  // Hiển thị thông báo khi không có quyền truy cập camera
  if (hasPermission === false) {
    return <Text>No access to camera</Text>;
  }

  return (
    <View style={styles.container}>
      {bookingDetails ? (
        // Nếu đã quét QR và có thông tin đặt chỗ
        <View style={styles.detailsContainer}>
          <Text style={styles.title}>Booking Details</Text>
          <Text>Invoice Number: {bookingDetails.invoiceNumber}</Text>
          <Text>Amount Paid: {bookingDetails.amountPaid}</Text>
          <Text>Status: {bookingDetails.status}</Text>
          <Text>Location: {bookingDetails.address}</Text>
          <Text>Date: {bookingDetails.dateStart} - {bookingDetails.dateEnd}</Text>

          {/* Hiển thị nút "Accept" nếu trạng thái là "Not yet" */}
          {bookingDetails.status === 'Not yet' && (
            <Button title="Accept" onPress={() => Alert.alert('Booking accepted')} />
          )}

          {/* Hiển thị nút quét lại nếu đã quét thành công */}
          <Button title="Scan Again" onPress={() => {
            setScanned(false); 
            setBookingDetails(null);  // Xóa dữ liệu khi quét lại
          }} />
        </View>
      ) : (
        // Nếu chưa quét hoặc chưa có thông tin đặt chỗ
        <Camera
          onBarCodeScanned={scanned ? undefined : handleBarCodeScanned}
          barCodeScannerSettings={{
            barCodeTypes: [Camera.Constants.BarCodeType.qr],
          }}
          style={StyleSheet.absoluteFillObject}
        />
      )}

      {/* Hiển thị nút quét lại nếu QR code đã được quét nhưng chưa có thông tin đặt chỗ */}
      {scanned && !bookingDetails && (
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
  detailsContainer: {
    padding: 20,
    alignItems: 'center',
  },
  title: {
    fontSize: 22,
    fontWeight: 'bold',
    marginBottom: 20,
  },
});

export default ScanQRCodeScreen;
