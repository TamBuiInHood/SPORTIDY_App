import React from 'react';
import { View, Text, Dimensions, StyleSheet } from 'react-native';
import { LineChart } from 'react-native-chart-kit';

const RevenueChart: React.FC = () => {
    const currentDate = new Date();


    // Format dates as a readable string
    const formatDate = (date: Date) => {
        const month = date.toLocaleString('default', { month: 'short' });
        const day = date.getDate();
        const year = date.getFullYear();
        return `${month} ${day}, ${year}`;
    };
    return (
        <View style={styles.chartContainer}>
            <Text style={styles.chartTitle}>
               {formatDate(currentDate)}
            </Text>
            <LineChart
                data={{
                    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                    datasets: [
                        {
                            data: [400, 300, 450, 500, 550, 400],
                            color: (opacity = 1) => `rgba(255, 99, 132, ${opacity})`,
                            strokeWidth: 2,
                        },
                        {
                            data: [300, 350, 400, 450, 500, 450], // Badminton data
                            color: (opacity = 1) => `rgba(53, 162, 235, ${opacity})`,
                            strokeWidth: 2,
                        },
                    ],
                    legend: ['Football', 'Badminton'],
                }}
                width={Dimensions.get('window').width - 40} // width from the device screen
                height={220}
                yAxisLabel=""
                chartConfig={{
                    backgroundColor: '#e26a00',
                    backgroundGradientFrom: '#fb8c00',
                    backgroundGradientTo: '#ffa726',
                    decimalPlaces: 0,
                    color: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
                    labelColor: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
                    style: {
                        borderRadius: 16,
                    },
                    propsForDots: {
                        r: '6',
                        strokeWidth: '2',
                        stroke: '#ffa726',
                    },
                }}
                style={{
                    marginVertical: 8,
                    borderRadius: 16,
                }}
            />
        </View>
    );
};

const styles = StyleSheet.create({
    chartContainer: {
        padding: 20,
        backgroundColor: '#fff',
        borderRadius: 12,
        marginBottom: 20,
        alignItems: 'center'
    },
    chartTitle: {
        fontSize: 16,
        fontWeight: 'bold',
        textAlign: 'center',
        marginBottom: 10,
    },
});

export default RevenueChart;
