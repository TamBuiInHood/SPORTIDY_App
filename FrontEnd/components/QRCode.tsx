import React, { forwardRef, useImperativeHandle } from 'react';
import QRCode from 'react-native-qrcode-svg';

// Create a wrapper component to handle the ref forwarding
const QRCodeWithRef = forwardRef((props, ref) => {
  const qrCodeRef = React.createRef();

  useImperativeHandle(ref, () => ({
    toDataURL: (callback: (data: string) => void) => {
      qrCodeRef.current?.toDataURL(callback);
    }
  }));

  return <QRCode {...props} ref={qrCodeRef} />;
});

export default QRCodeWithRef;
