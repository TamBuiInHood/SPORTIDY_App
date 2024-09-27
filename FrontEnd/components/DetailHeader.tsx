import React from 'react';
import { StyleSheet, View, Text, TouchableOpacity } from 'react-native';

interface DetailHeaderProps {
    date: string;
    title: string;
    activeTab: 'details' | 'discussion';
    onTabChange: (newTab: 'details' | 'discussion') => void;
}

const DetailHeader: React.FC<DetailHeaderProps> = ({ date, title, activeTab, onTabChange }) => {
    return (
        <View style={styles.header}>
            <Text style={styles.headerDate}>{date}</Text>
            <Text style={styles.headerTitle}>{title}</Text>
            <View style={styles.tabContainer}>
                <TouchableOpacity
                    style={activeTab === 'details' ? styles.activeTab : styles.inactiveTab}
                    onPress={() => onTabChange('details')}>
                    <Text style={activeTab === 'details' ? styles.tabTextActive : styles.tabTextInactive}>
                        Details
                    </Text>
                </TouchableOpacity>
                <TouchableOpacity
                    style={activeTab === 'discussion' ? styles.activeTab : styles.inactiveTab}
                    onPress={() => onTabChange('discussion')}>
                    <Text style={activeTab === 'discussion' ? styles.tabTextActive : styles.tabTextInactive}>
                        Discussion
                    </Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    header: {
        backgroundColor: '#FFB41C',
        padding: 20,
        marginBottom: 15,
        alignItems: 'center',
        justifyContent: 'center',
    },
    headerDate: {
        fontSize: 14,
        color: '#FF915D',
        marginTop: 30,
        fontWeight: "bold",
        textAlign: 'center',
    },
    headerTitle: {
        fontSize: 19,
        color: '#fff',
        fontWeight: 'bold',
        marginBottom: 15,
        textAlign: 'center',
    },
    tabContainer: {
        flexDirection: 'row',
        justifyContent: 'space-evenly', 
        alignItems: 'center',
        width: '100%',
    },
    activeTab: {
        flex: 1,
        paddingHorizontal: 15,
        backgroundColor: '#FFD01C',
        paddingVertical: 10,
        borderRadius: 10,
        alignItems: 'center',
    },
    inactiveTab: {
        flex: 1, 
        backgroundColor: 'transparent',
        paddingVertical: 10,
        alignItems: 'center',
    },
    tabTextActive: {
        color: '#fff',
        fontWeight: 'bold',
        fontSize: 15,
    },
    tabTextInactive: {
        color: '#fff',
        fontWeight: 'bold',
        fontSize: 15,
    },
});

export default DetailHeader;
