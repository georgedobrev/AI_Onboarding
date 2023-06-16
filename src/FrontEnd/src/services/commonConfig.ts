export const authHeader = () => {
    const accessToken = localStorage.getItem('accessToken');

    return {
        headers: {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'application/json',
        },
    };
};