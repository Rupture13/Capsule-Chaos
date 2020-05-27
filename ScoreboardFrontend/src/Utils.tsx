import { T } from "antd/lib/upload/utils";

class Utils {
	FormatDate(item) {
		return new Date(Date.parse(item)).toDateString();
	}

	GetArrayCopy<T>(originalArray: T[]) {
		return originalArray.slice(0, originalArray.length);
	}
}
export default new Utils();