# oopproject2

## 1:03 AM 5/26/2021
### Module Mặt Hàng
- Thêm, Tìm Kiếm

## 20:00 26/05/2021
### Module Mặt Hàng
- Sửa, Xóa

### Module Loại Hàng
- Thêm, Tìm Kiếm, Sửa, Xóa

### Module Hóa Đơn Nhập Hàng
- Danh sách

## 19:57 27/05/2021
### Module Hóa Đơn Nhập Hàng
- Thêm, Tìm kiếm, Sửa, Xóa

### Module Hóa Đơn Bán Hàng
- Thêm, Tìm kiếm, Sửa, Xóa

## 10:42 PM 5/28/2021
### Module Thống Kê
- Thống kê số lượng hàng theo thể loại hàng
- Thống kê số lượng hàng hết hạn theo mặt hàng

## 2:06 PM 5/29/2021
### Module THÊM MẶT HÀNG
- Sử dụng Cookie để lưu tạm dữ liệu người dùng, để người dùng đỡ phải tốn công nhập lại ở phần Frontend nếu như phát sinh validate không thành công
- Thêm phần validate dữ liệu người dùng nhập rồi thông báo ra ngoài Frontend cho người dùng biết

## 11:11 PM 5/29/2021
### Module SỬA, XÓA MẶT HÀNG
- Thêm validate dữ liệu người dùng rồi hiển thị ra ngoài Frontend

## 3:16 AM 5/30/2021
### Module THÊM, SỬA, XÓA LOẠI HÀNG
- Validate dữ liệu trước khi thêm
- Kiểm tra mặt hàng có thuộc loại hàng đang cần xóa hay không, nếu có thì update giá trị loại hàng của các mặt hàng này về "Chưa phân loại"

### Module THÊM HÓA ĐƠN NHẬP HÀNG
- Validate dữ liệu trước khi thêm

##11:17 AM 5/30/2021
### Module SỬA, XÓA HÓA ĐƠN NHẬP HÀNG
- Validate dữ liệu trước khi sửa
- Trước khi xóa kiểm tra tính hợp lệ của dữ liệu, nếu tổng số lượng nhập bé hơn tổng số lượng bán thì không cho phép xóa. Bắt buộc phải điều chỉnh lại một trong 2 cái để hiệu số lại phải lớn hơn 0.

## 12:20 PM 5/30/2021
### TỔNG KẾT

### Module SỬA HÓA ĐƠN NHẬP HÀNG
- Kiểm tra tính logic, nếu số lượng nhập làm cho tổng số hàng đã nhập trừ đi cho tổng hàng đã bán < 0 thì không cho sửa
