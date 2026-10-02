Câu 1:
1. Phân loại các kiểu dữ liệu
-Value Types (Kiểu giá trị): Bao gồm các kiểu dữ liệu nguyên thủy như int, float, double, bool, char, các cấu trúc do người dùng định nghĩa (struct) và kiểu liệt kê (enum).
-Reference Types (Kiểu tham chiếu): Bao gồm các lớp (class), giao diện (interface), delegate, string, object và các kiểu mảng (array).

2. Cơ chế lưu trữ vùng nhớ (Stack vs Heap)
-Value Types:
Giá trị thực sự của biến được lưu trực tiếp tại vùng nhớ Stack.
Ngoại lệ: Nếu một biến Value Type là một thuộc tính hoặc trường (field) bên trong một đối tượng Reference Type, nó sẽ được lưu Inline ngay bên trong đối tượng đó ở vùng nhớ Heap.
-Reference Types:
Cơ chế lưu trữ được chia làm hai phần:
Heap: Dùng để lưu trữ đối tượng thực sự (chứa toàn bộ dữ liệu và thuộc tính của đối tượng).
Stack: Dùng để lưu trữ biến tham chiếu (con trỏ địa chỉ), biến này chứa địa chỉ ô nhớ dẫn tới vị trí của đối tượng trên Heap.

3. Cơ chế sao chép và gán dữ liệu
-Value Types: Khi thực hiện phép gán (ví dụ: b = a), chương trình sẽ sao chép toàn bộ giá trị từ a sang b. Hai biến a và b hoàn toàn độc lập ở hai ô nhớ riêng biệt trên Stack. Việc thay đổi giá trị của b không làm ảnh hưởng đến a.
-Reference Types: Khi thực hiện phép gán (ví dụ: b = a), chương trình chỉ sao chép địa chỉ tham chiếu từ a sang b. Cả hai biến lúc này cùng trỏ vào một đối tượng duy nhất trên Heap. Do đó, bất kỳ sự thay đổi dữ liệu nào thông qua b cũng sẽ trực tiếp làm thay đổi dữ liệu mà a đang tham chiếu tới.

4. Cơ chế quản lý bộ nhớ
-Value Types: Được giải phóng bộ nhớ tự động ngay khi biến đó ra khỏi phạm vi hoạt động (Scope) của hàm hoặc khối lệnh.
-Reference Types: Việc giải phóng vùng nhớ Heap do bộ thu gom rác Garbage Collector (GC) của .NET tự động thực hiện khi phát hiện đối tượng đó không còn biến tham chiếu nào trỏ tới.

5. Khả năng nhận giá trị null
-Value Types: Mặc định không thể nhận giá trị null (trừ khi được khai báo dưới dạng Nullable Types, ví dụ: int?).
-Reference Types: Mặc định có thể nhận giá trị null khi biến chưa trỏ tới bất kỳ đối tượng nào trên Heap.

Câu 2:
1. Sự khác biệt giữa init và set thông thường
-Thuộc tính có set thông thường (Mutable):
Cho phép gán hoặc thay đổi lại giá trị của thuộc tính ở bất kỳ thời điểm nào trong suốt vòng đời của đối tượng.
Việc thay đổi giá trị có thể thực hiện lúc khởi tạo, trong hàm khởi tạo (Constructor), hoặc ở bất kỳ phương thức nào sau khi đối tượng đã được tạo xong.
-Thuộc tính có init (Init-only Setter - Immutable):
Chỉ cho phép gán giá trị một lần duy nhất trong quá trình khởi tạo đối tượng (thông qua Constructor hoặc Object Initializer { Property = Value }).
Ngay khi quá trình khởi tạo đối tượng kết thúc, thuộc tính sẽ tự động trở thành Read-only (chỉ đọc). Nếu cố tình gán lại giá trị ở các dòng lệnh sau đó, chương trình sẽ báo lỗi biên dịch (Compile-time error).

2. Trường hợp sử dụng thực tế
-Tạo các đối tượng bất biến (Immutable Objects): Đảm bảo dữ liệu của đối tượng không bị vô tình sửa đổi trong quá trình thực thi chương trình, giúp mã nguồn an toàn hơn khi xử lý đa luồng (Thread-safety).
-DTO (Data Transfer Objects) và API Models: Khai báo các lớp đại diện cho dữ liệu nhận về từ API hoặc Database, nơi dữ liệu chỉ cần đọc và không được phép chỉnh sửa sau khi khởi tạo.
-Các thuộc tính định danh không thay đổi: Khai báo các thuộc tính gắn liền với định danh của đối tượng như Id, CreatedDate, SSN (Số CMND/CCCD).

Câu 3:
1. Khái niệm và Vai trò
-Phương thức virtual (ở lớp cha - Base Class):
Được sử dụng để đánh dấu một phương thức ở lớp cha, cho biết phương thức này cho phép các lớp con có thể ghi đè (tái định nghĩa) lại hành vi nếu cần.
Phương thức virtual bắt buộc phải có phần thân hàm (body/implementation) mặc định ở lớp cha.
-Phương thức override (ở lớp con - Derived Class):
Được sử dụng ở lớp con để chính thức ghi đè và thay thế logic định nghĩa của phương thức virtual từ lớp cha truyền xuống.
Từ khóa override thể hiện sự định nghĩa lại hành vi cụ thể dành riêng cho lớp con.

2. So sánh chi tiết
-Vị trí khai báo: Phương thức virtual nằm ở lớp cha (Base Class), còn phương thức override nằm ở lớp con (Derived Class).
-Tính bắt buộc:
Lớp con không bắt buộc phải dùng override đối với phương thức virtual. Nếu không ghi đè, lớp con sẽ tự động sử dụng lại logic mặc định của lớp cha.
Phương thức override bắt buộc phải tương ứng với một phương thức virtual (hoặc abstract) đã được khai báo ở lớp cha.
-Tên hàm và chữ ký hàm (Method Signature): Phương thức override ở lớp con phải giữ nguyên tên, kiểu trả về và danh sách tham số giống hệt với phương thức virtual ở lớp cha.

3. Cơ chế Đa hình tại thời điểm chạy (Runtime Polymorphism)
Khi gọi một phương thức thông qua biến tham chiếu có kiểu dữ liệu là lớp cha nhưng đang trỏ tới một đối tượng thuộc lớp con, trình biên dịch C# dựa vào từ khóa override để thực thi phương thức của lớp con tại thời điểm chạy (Runtime) thay vì phương thức của lớp cha.

Câu 4:
1. Bản chất sở hữu ở cấp độ Lớp (Class-level vs Instance-level)
-Thành phần static: Thuộc sở hữu của chính Lớp (Class) đó. Khi chương trình thực thi, chỉ có duy nhất một bản sao (Single Instance) của thành phần static tồn tại trong bộ nhớ và được dùng chung cho toàn bộ ứng dụng.
-Thể hiện (Object Instance): Được tạo ra bằng toán tử new, đại diện cho một đối tượng cụ thể mang dữ liệu và trạng thái riêng biệt. Mỗi thể hiện có vùng nhớ độc lập chứa các thuộc tính/phương thức thông thường (non-static).

2. Cơ chế quản lý bộ nhớ và biên dịch (Memory & Compiler)
-Thời điểm cấp phát vùng nhớ: Vùng nhớ cho thành phần static được hệ thống cấp phát ngay khi Lớp được nạp vào bộ nhớ (High Frequency Heap) trước khi bất kỳ thể hiện nào được tạo ra bằng toán tử new.
-Cơ chế truyền ngữ cảnh (Context): Trình biên dịch không truyền con trỏ this (tham chiếu trỏ tới thể hiện hiện tại) vào các phương thức hoặc thuộc tính static. Do đó, thành phần static hoạt động độc lập và hoàn toàn không có ngữ cảnh của một Object Instance cụ thể.

3. Triết lý thiết kế ngôn ngữ C#
-Tránh nhầm lẫn dữ liệu: Nếu C# cho phép truy xuất static qua thể hiện (như instance.StaticMember), lập trình viên dễ bị hiểu nhầm rằng thành phần đó mang giá trị riêng của thể hiện đó.
-Đảm bảo tính tường minh (Explicit Code): Bắt buộc truy xuất thông qua tên Lớp (ClassName.StaticMember) giúp phân biệt rõ ràng giữa:
Dữ liệu / Hành vi dùng chung cho toàn bộ Class.
Dữ liệu / Hành vi riêng biệt của từng đối tượng cụ thể.
