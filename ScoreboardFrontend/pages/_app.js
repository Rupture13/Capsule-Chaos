import React from 'react';
import App from 'next/app';
import 'antd/dist/antd.css';
class MyApp extends App {
    componentDidMount() {
        // Remove the server-side injected CSS.
        const jssStyles = document.querySelector('#jss-server-side');
        if (jssStyles) {
            jssStyles.parentElement.removeChild(jssStyles);
        }
    }

    render() {
        const { Component, pageProps } = this.props;

        return (
            <React.Fragment>
                    <Component {...pageProps} />
            </React.Fragment>
        );
    }
}
export default MyApp
