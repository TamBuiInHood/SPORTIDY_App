// import React from 'react';
// import { View, Text, StyleSheet, TouchableOpacity, Alert } from 'react-native';
// import QRCodeScanner from 'react-native-qrcode-scanner';
// import { RNCamera } from 'react-native-camera';

// const ScanQRCodeScreen = () => {
//   const onSuccess = (e: any) => {
//     Alert.alert('QR Code Scanned!', e.data);
//   };

//   return (
//     <View style={styles.container}>
//       <QRCodeScanner
//         onRead={onSuccess}
//         flashMode={RNCamera.Constants.FlashMode.auto}
//         topContent={<Text style={styles.centerText}>Scan the QR Code</Text>}
//         bottomContent={
//           <TouchableOpacity style={styles.buttonTouchable}>
//             <Text style={styles.buttonText}>OK, Got it!</Text>
//           </TouchableOpacity>
//         }
//       />
//     </View>
//   );
// };

// const styles = StyleSheet.create({
//   container: {
//     flex: 1,
//     justifyContent: 'center',
//     alignItems: 'center',
//   },
//   centerText: {
//     fontSize: 18,
//     margin: 32,
//     textAlign: 'center',
//   },
//   buttonTouchable: {
//     padding: 16,
//     backgroundColor: '#48C9B0',
//     borderRadius: 8,
//   },
//   buttonText: {
//     color: '#fff',
//     fontWeight: 'bold',
//   },
// });

// export default ScanQRCodeScreen;
import { View, Text } from 'react-native'
import React from 'react'

const scan = () => {
  return (
    <View>
      <Text>scan</Text>
    </View>
  )
}

export default scan