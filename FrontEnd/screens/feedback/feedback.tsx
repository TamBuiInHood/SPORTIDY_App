import React, { useState } from 'react';
import { View, Text, StyleSheet, TextInput, TouchableOpacity, Switch, Image, Alert } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { AirbnbRating } from 'react-native-ratings';
import * as ImagePicker from 'expo-image-picker'; // Thư viện tải ảnh/video

const FeedbackPage: React.FC = ({ route }) => {
    const { booking } = route.params;

    // State cho đánh giá và feedback
    const [rating, setRating] = useState(5);
    const [feedback, setFeedback] = useState('');
    const [showAccountName, setShowAccountName] = useState(true);
    const [image, setImage] = useState(null);
    const [video, setVideo] = useState(null);


    // Hàm tải hình ảnh
    const pickImage = async () => {
        let result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.Images,
            allowsEditing: true,
            aspect: [4, 3],
            quality: 1,
        });
        if (!result.canceled) {
            setImage(result.uri);
        }
    };

    // Hàm tải video
    const pickVideo = async () => {
        let result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.Videos,
            allowsEditing: true,
            quality: 1,
        });
        if (!result.canceled) {
            setVideo(result.uri);
        }
    };

    // Hàm xử lý gửi feedback
    const handleSubmitFeedback = () => {
        Alert.alert('Feedback Sent!', 'Thank you for your feedback.');
        // Logic gửi feedback lên server...
    };

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Feedback Playfield</Text>


            <View style={styles.detailContainer}>
                <View style={styles.header}>
                    <Ionicons name="bookmark-outline" size={24} color="#ff951d" style={styles.icon} />
                    <Text style={styles.fieldName}>{booking.playfieldName}</Text>
                    <View style={styles.statusBadge}>
                        <Text style={styles.statusText}>{booking.status}</Text>
                    </View>
                </View>

                <Text style={styles.invoiceNumber}>Invoice Number: {booking.invoiceNumber}</Text>
                <Text style={styles.address}>Address: {booking.address}</Text>
                <Text style={styles.price}>Price: {booking.price.toLocaleString()} VND</Text>
            </View>

            {/* Đánh giá sao */}
            <View style={styles.ratingContainer}>
                <Text style={styles.ratingLabel}>Field quality</Text>
                <AirbnbRating
                    count={5}
                    defaultRating={rating}
                    onFinishRating={(value) => setRating(value)}
                    size={30}
                    showRating={false}
                />
                <Text style={styles.ratingText}>{rating === 5 ? 'Perfect' : ''}</Text>
            </View>

            {/* Tải hình ảnh và video */}
            <View style={styles.uploadContainer}>
                <TouchableOpacity style={styles.uploadButton} onPress={pickImage}>
                    <Ionicons name="image-outline" size={24} color="#ff951d" />
                    <Text>Image</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.uploadButton} onPress={pickVideo}>
                    <Ionicons name="videocam-outline" size={24} color="#ff951d" />
                    <Text>Video</Text>
                </TouchableOpacity>
            </View>

            {image && <Image source={{ uri: image }} style={styles.mediaPreview} />}
            {video && <Text style={styles.videoPreview}>Video selected</Text>}

            {/* Input feedback */}
            <TextInput
                style={styles.input}
                multiline
                numberOfLines={4}
                placeholder="Leave your review here..."
                value={feedback}
                onChangeText={setFeedback}
            />

            {/* Hiển thị tên tài khoản */}
            <View style={styles.switchContainer}>
                <Text>Show name of your account in this feedback</Text>
                <Switch value={showAccountName} onValueChange={setShowAccountName} />
            </View>

            {/* Nút gửi feedback */}
            <TouchableOpacity style={styles.submitButton} onPress={handleSubmitFeedback}>
                <Text style={styles.submitButtonText}>Send Feedback</Text>
            </TouchableOpacity>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        textAlign: 'center',
        marginVertical: 20,
        color: '#FFA500',
    },
    detailContainer: {
        backgroundColor: '#F5F5F5',
        padding: 15,
        borderRadius: 10,
        marginBottom: 20,
    },
    fieldName: {
        fontWeight: 'bold',
        fontSize: 18,
    },
    invoiceNumber: {
        color: '#FF4500',
        marginVertical: 5,
    },
    address: {
        color: '#555',
    },
    price: {
        color: '#000',
        marginTop: 5,
    },
    status: {
        color: 'green',
    },
    ratingContainer: {
        alignItems: 'center',
        marginVertical: 20,
    },
    ratingLabel: {
        fontWeight: 'bold',
        fontSize: 16,
        marginBottom: 10,
    },
    ratingText: {
        marginTop: 10,
        fontSize: 16,
        color: '#FF951D',
    },
    uploadContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        marginVertical: 20,
    },
    uploadButton: {
        alignItems: 'center',
        backgroundColor: '#FFF5E0',
        padding: 10,
        borderRadius: 8,
        width: '45%',
    },
    mediaPreview: {
        width: '100%',
        height: 200,
        borderRadius: 10,
        marginTop: 10,
    },
    videoPreview: {
        marginTop: 10,
        textAlign: 'center',
        color: '#FF951D',
    },
    input: {
        backgroundColor: '#F5F5F5',
        padding: 15,
        borderRadius: 10,
        marginVertical: 20,
        textAlignVertical: 'top',
    },
    switchContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginVertical: 10,
    },
    submitButton: {
        backgroundColor: '#FF951D',
        padding: 15,
        borderRadius: 10,
        alignItems: 'center',
        marginTop: 20,
    },
    submitButtonText: {
        color: '#fff',
        fontSize: 18,
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 10
      },
      statusBadge: {
        backgroundColor: '#e6ffe6',  // light green for "Used" status
        borderRadius: 10,
        paddingVertical: 4,
        paddingHorizontal: 8,
      },
      statusText: {
        color: '#4CAF50',  // green text
        fontWeight: 'bold',
      },
});

export default FeedbackPage;
