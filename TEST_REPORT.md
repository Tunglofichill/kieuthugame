# Báo Cáo Kịch Bản Automation Test - Dự Án Tower Defense

Dự án đã tích hợp sẵn Unity Test Framework và cấu hình 2 kịch bản tự động kiểm tra (Automation Test) trong chế độ PlayMode để đảm bảo các tính năng cốt lõi hoạt động đúng mong đợi.

## 1. Test Case 1: Kẻ Địch Đi Đến Đích (Enemy Pathing & Lives)
- **Tệp chứa mã nguồn test:** `Assets/Tests/PlayModeTests/EnemyPathTest.cs`
- **Mô tả chức năng:** Kiểm tra hệ thống di chuyển của quái vật trên đường đi (Waypoints). Khi quái vật đi đến vị trí cuối cùng, nó phải bị tiêu diệt và trừ đi 1 mạng (Lives) của người chơi.
- **Tiến trình chạy Test:**
  1. Tải màn chơi cốt lõi (`game.unity`).
  2. Instantiate (tạo mới) một GameObject Quái Vật ở điểm bắt đầu (Waypoint 0).
  3. Gọi hàm giả lập `SimulateReachLastWaypoint()` đưa quái vật tới lưới đích.
  4. Xác nhận số lượng mạng (Lives) hiện tại đã bị giảm đúng 1 đơn vị so với lúc ban đầu.
- **Kết quả mong đợi:** `Passed`

## 2. Test Case 2: Tiêu Tiền Mua Tháp Thỏ (Tower Placement & Economy)
- **Tệp chứa mã nguồn test:** `Assets/Tests/PlayModeTests/TowerPlacementTest.cs`
- **Mô tả chức năng:** Kiểm tra hệ thống kéo/thả đặt Tháp (Thỏ) và cơ chế tiêu thụ tài nguyên. Khi người chơi đủ tiền và đặt tháp xuống một vị trí hợp lệ, tiền phải bị trừ đúng với giá của Tháp.
- **Tiến trình chạy Test:**
  1. Tải màn chơi.
  2. Bơm tiền cho người chơi để đảm bảo số dư >= 50 tiền (vượt mốc giá trị của 1 tháp).
  3. Gọi hàm giả lập `SimulateValidTowerPlacement(Vector2)` đặt một Tháp Thỏ ở tọa độ nền (Background) hợp lệ.
  4. Xác nhận số lượng Tiền (MoneyAvailable) đã bị trừ đi chính xác 50 điểm.
- **Kết quả mong đợi:** `Passed`

---

## Hướng Dẫn Kích Hoạt Test Trong Unity (Cách Chạy)

Để giám khảo/mentor kiểm chứng kết quả chạy test, xin thực hiện các bước sau:
1. Mở dự án Unity bằng **Unity Editor**.
2. Trên thanh công cụ trên cùng, chọn tab: **Window** > **General** > **Test Runner**.
3. Cửa sổ `Test Runner` hiện ra, hãy chọn thẻ **PlayMode**.
4. Bấm nút **Run All** (hoặc chọn từng test case rời bên dưới và bấm *Run Selected*).
5. Unity sẽ tự động Play game, chạy giả lập các thao tác tự động trên và hiển thị dấu Tick Xanh (Passed) cho cả 2 chức năng trên cửa sổ Test Runner.
