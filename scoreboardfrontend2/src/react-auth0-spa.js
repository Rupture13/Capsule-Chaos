import React, { useState, useContext } from "react";

const DEFAULT_REDIRECT_CALLBACK = () =>
	window.history.replaceState({}, document.title, window.location.pathname);

export const Auth0Context = React.createContext();
export const useAuth0 = () => useContext(Auth0Context);
export const Auth0Provider = ({
	children,
	onRedirectCallback = DEFAULT_REDIRECT_CALLBACK,
	...initOptions
}) => {
	const [isAuthenticated, setIsAuthenticated] = useState();
	const [user, setUser] = useState();
	const [loading, setLoading] = useState(true);
	return (
		<Auth0Context.Provider
			value={{
				isAuthenticated,
				user,
				loading,
				logout: (...p) => { user = undefined }
			}}
		>
			{children}
		</Auth0Context.Provider>
	);
};