// src/components/PrivateRoute.js

import React, { useEffect } from "react";
import { Route } from "react-router-dom";
import { useAuth0 } from "../react-auth0-spa";

const PrivateRoute = ({ component: Component, path, ...rest }) => {
	const { loading, isAuthenticated } = useAuth0();

	useEffect(() => {
		if (loading || isAuthenticated) {
			return;
		}
	}, [loading, isAuthenticated, path]);

	const render = props =>
		isAuthenticated === true ? <Component {...props} /> : null;

	return <Route path={path} render={render} {...rest} />;
};

export default PrivateRoute;