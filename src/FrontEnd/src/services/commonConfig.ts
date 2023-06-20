export const authHeaderAI = () => {
    const accessToken = localStorage.getItem('accessToken');

    return {
        headers: {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'application/json',
        },
    };
};

export const authHeaderFile = () => {
    const accessToken = localStorage.getItem('accessToken');

    return {
        headers: {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'multipart/form-data',
        },
    };
}