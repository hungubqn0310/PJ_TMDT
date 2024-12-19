document.addEventListener("DOMContentLoaded", function () {
    const musicPlayer = document.querySelector(".music-player");
    const toggleBtn = document.querySelector(".toggle-player-btn");
    const toggleIcon = document.querySelector("#toggle-icon");

    toggleBtn.addEventListener("click", function () {
        musicPlayer.classList.toggle("closed");

        // Thay đổi icon giữa chevron-up và chevron-down
        if (musicPlayer.classList.contains("closed")) {
            toggleIcon.classList.remove("fa-chevron-up");
            toggleIcon.classList.add("fa-chevron-down");
        } else {
            toggleIcon.classList.remove("fa-chevron-down");
            toggleIcon.classList.add("fa-chevron-up");
        }
    });
});
	document.addEventListener("DOMContentLoaded", function () {
    const audioPlayer = document.getElementById('audio-player');
    const showSongListBtn = document.getElementById('show-song-list-btn');
    const songListContainer = document.getElementById('song-list-container');
    const songListUl = document.getElementById('song-list');
    const songTitle = document.getElementById('song-title'); // Phần tử hiển thị tên bài hát
    const playPauseBtn = document.getElementById('play-pause-btn'); // Nút play/pause

    let songList = []; // Danh sách bài hát, sẽ được tải từ API hoặc mảng tĩnh
    let currentSongIndex = sessionStorage.getItem('currentSongIndex') || 0; // Lấy chỉ mục bài hát đã chọn từ sessionStorage

    // Giả sử bạn đã lấy danh sách bài hát từ API
    fetch('/api/music') // Đường dẫn lấy danh sách bài hát
        .then(response => response.json())
        .then(data => {
            songList = data;
            displaySongList(); // Hiển thị danh sách bài hát
            loadSong(songList[currentSongIndex]); // Tải bài hát hiện tại từ sessionStorage
        })
        .catch(error => console.log('Error loading music list:', error));

    // Hiển thị danh sách bài hát
    function displaySongList() {
        songListUl.innerHTML = ''; // Xóa danh sách cũ
        songList.forEach((song, index) => {
            const li = document.createElement('li');
            li.textContent = song.replace(/\.mp3$/, ''); // Hiển thị tên bài hát

            

            li.addEventListener('click', () => {
                currentSongIndex = index; // Cập nhật chỉ mục bài hát đã chọn
                sessionStorage.setItem('currentSongIndex', currentSongIndex); // Lưu lại chỉ mục bài hát
                loadSong(song); // Khi chọn bài, phát bài đó
                audioPlayer.play(); // Phát bài hát ngay lập tức
                displaySongList(); // Cập nhật danh sách để làm nổi bật bài hát đang phát
                updatePlayPauseButton(true); // Đặt nút play/pause thành pause
            });
            songListUl.appendChild(li);
        });
    }

    // Tải và phát bài hát
    function loadSong(song) {
        const songUrl = `/audio/${song}`;
        audioPlayer.src = songUrl;
        audioPlayer.load(); // Nạp lại bài nhạc khi thay đổi

        // Cập nhật tên bài hát hiển thị trong songTitle
        const songTitleText = song.replace(/\.mp3$/, ''); // Xử lý tên bài hát
        if (songTitle) {
            songTitle.textContent = songTitleText; // Cập nhật nội dung tên bài hát
        }
    }

    // Cập nhật trạng thái nút play/pause
    function updatePlayPauseButton(isPlaying) {
        if (isPlaying) {
            playPauseBtn.classList.remove('fa-play'); // Xóa biểu tượng play
            playPauseBtn.classList.add('fa-pause'); // Thêm biểu tượng pause
        } else {
            playPauseBtn.classList.remove('fa-pause'); // Xóa biểu tượng pause
            playPauseBtn.classList.add('fa-play'); // Thêm biểu tượng play
        }
    }

    // Khi nhấn nút play/pause
    playPauseBtn.addEventListener('click', () => {
        if (audioPlayer.paused) {
            audioPlayer.play();
            updatePlayPauseButton(true); // Chuyển sang trạng thái pause
        } else {
            audioPlayer.pause();
            updatePlayPauseButton(false); // Chuyển sang trạng thái play
        }
    });

    // Thêm animation hiển thị/ẩn danh sách bài hát
    function toggleSongListContainer() {
        if (songListContainer.classList.contains('show')) {
            songListContainer.classList.remove('show'); // Ẩn danh sách
            setTimeout(() => {
                songListContainer.style.display = 'none';
            }, 300); // Khớp với thời gian CSS transition
        } else {
            songListContainer.style.display = 'block';
            setTimeout(() => {
                songListContainer.classList.add('show'); // Hiển thị danh sách
            }, 10);
        }
    }

    // Khi nhấn nút, hiển thị/ẩn danh sách bài hát
    showSongListBtn.addEventListener('click', (e) => {
        e.stopPropagation(); // Ngừng sự kiện click để không bị kích hoạt cho document
        toggleSongListContainer();
    });

    // Khi nhấn ra ngoài songListContainer, ẩn danh sách bài hát
    document.addEventListener('click', (e) => {
        if (!songListContainer.contains(e.target) && e.target !== showSongListBtn) {
            if (songListContainer.classList.contains('show')) {
                toggleSongListContainer();
            }
        }
    });

    // Ngừng sự kiện khi nhấn vào #song-list-container để không đóng khi click bên trong
    songListContainer.addEventListener('click', (e) => {
        e.stopPropagation();
    });
});



    document.addEventListener("DOMContentLoaded", function () {
    const audioPlayer = document.getElementById('audio-player');
    const playPauseBtn = document.getElementById('play-pause-btn');
    const progressBar = document.getElementById('progress-bar');
    const currentTimeDisplay = document.getElementById('current-time');
    const durationDisplay = document.getElementById('duration');
    const volumeControl = document.getElementById('volume-control');
    const prevBtn = document.getElementById('prev-btn');
    const nextBtn = document.getElementById('next-btn');
    const repeatBtn = document.getElementById('repeat-btn');
    const shuffleBtn = document.getElementById('shuffle-btn');
    const songTitle = document.getElementById('song-title');

    let isPlaying = false;
    let isRepeat = false;
    let isShuffle = false;
    let songList = [];
    let currentSongIndex = 0;

	// Khôi phục trạng thái Shuffle và Repeat từ sessionStorage
    const savedIsShuffle = sessionStorage.getItem('isShuffle');
    if (savedIsShuffle !== null) {
        isShuffle = savedIsShuffle === 'true';
        shuffleBtn.classList.toggle('active', isShuffle);
        shuffleBtn.classList.toggle('inactive', !isShuffle);
    }

    const savedIsRepeat = sessionStorage.getItem('isRepeat');
    if (savedIsRepeat !== null) {
        isRepeat = savedIsRepeat === 'true';
        audioPlayer.loop = isRepeat;
        repeatBtn.classList.toggle('active', isRepeat);
        repeatBtn.classList.toggle('inactive', !isRepeat);
    }
    // Lấy danh sách các bài nhạc từ API
    fetch('/api/music')
        .then(response => response.json())
        .then(data => {
            songList = data;
            if (songList.length > 0) {
                const savedSongIndex = sessionStorage.getItem('currentSongIndex');
                if (savedSongIndex) {
                    currentSongIndex = parseInt(savedSongIndex);
                } else {
                // Chọn ngẫu nhiên bài hát khi vào lần đầu
                currentSongIndex = Math.floor(Math.random() * songList.length);
                sessionStorage.setItem('currentSongIndex', currentSongIndex); // Lưu lại bài hát ngẫu nhiên đã chọn
            }

                loadSong(songList[currentSongIndex]);

                // Kiểm tra và phục hồi thời gian phát nhạc đã lưu
                const savedTime = sessionStorage.getItem('audioPlayerCurrentTime');
                if (savedTime) {
                    audioPlayer.currentTime = savedTime;
                }

                // Kiểm tra và phục hồi âm lượng
                const savedVolume = sessionStorage.getItem('audioPlayerVolume');
                if (savedVolume) {
                    audioPlayer.volume = savedVolume;
                    volumeControl.value = savedVolume * 100;
                }

                // Kiểm tra trạng thái phát nhạc (tạm dừng hay đang phát)
                const savedIsPlaying = sessionStorage.getItem('isPlaying');
                if (savedIsPlaying === 'true') {
                    audioPlayer.play().then(() => {
                        isPlaying = true;
                        playPauseBtn.classList.remove('fa-play');
                        playPauseBtn.classList.add('fa-pause');
                    }).catch(err => {
                        console.log('Error starting playback:', err);
                    });
                } else {
                    isPlaying = false;
                    playPauseBtn.classList.remove('fa-pause');
                    playPauseBtn.classList.add('fa-play');
                }
            }
        })
        .catch(error => console.log('Error loading music list:', error));

    // Cập nhật thông tin bài nhạc
    function loadSong(song) {
        const songUrl = `/audio/${song}`;
        audioPlayer.src = songUrl;
        audioPlayer.load(); // Nạp lại bài nhạc khi thay đổi
        songTitle.textContent = song.replace(/\.mp3$/, '');
    }
    
    // Cập nhật thời gian và thanh tiến độ
    audioPlayer.addEventListener('timeupdate', () => {
        if (audioPlayer.duration && audioPlayer.currentTime) {
            const currentTime = Math.floor(audioPlayer.currentTime);
            const duration = Math.floor(audioPlayer.duration);

            // Cập nhật thanh tiến độ
            progressBar.value = currentTime;

            // Cập nhật thời gian hiện tại
            const currentMinutes = Math.floor(currentTime / 60);
            const currentSeconds = currentTime % 60;
            currentTimeDisplay.textContent = `${currentMinutes}:${currentSeconds < 10 ? '0' + currentSeconds : currentSeconds}`;

            // Cập nhật thời gian tổng của bài hát
            const durationMinutes = Math.floor(duration / 60);
            const durationSeconds = duration % 60;
            durationDisplay.textContent = `${durationMinutes}:${durationSeconds < 10 ? '0' + durationSeconds : durationSeconds}`;
        }
    });

    // Cập nhật giá trị max cho thanh tiến độ khi bài hát được nạp
    audioPlayer.addEventListener('loadedmetadata', () => {
        const duration = Math.floor(audioPlayer.duration);
        progressBar.max = duration; // Đặt giá trị max của thanh tiến độ theo thời gian bài hát
    });

    // Khi người dùng kéo thanh tiến độ, cập nhật vị trí phát nhạc
    progressBar.addEventListener('input', () => {
        audioPlayer.currentTime = progressBar.value;
    });
// Khi bài hát kết thúc, chuyển sang bài tiếp theo
    audioPlayer.addEventListener('ended', () => {
        // Tự động chuyển đến bài tiếp theo
        currentSongIndex = (currentSongIndex + 1) % songList.length;
        loadSong(songList[currentSongIndex]);
        audioPlayer.play();
        isPlaying = true;
        playPauseBtn.classList.remove('fa-play');
        playPauseBtn.classList.add('fa-pause');
		sessionStorage.setItem('currentSongIndex', currentSongIndex); // Lưu lại bài hát hiện tại
        sessionStorage.setItem('isPlaying', 'true'); // Lưu trạng thái đang phát
    });
    // Play/Pause
    playPauseBtn.addEventListener('click', () => {
        if (isPlaying) {
            audioPlayer.pause();
            playPauseBtn.classList.remove('fa-pause');
            playPauseBtn.classList.add('fa-play');
            sessionStorage.setItem('isPlaying', 'false'); // Lưu trạng thái tạm dừng
        } else {
            audioPlayer.play();
            playPauseBtn.classList.remove('fa-play');
            playPauseBtn.classList.add('fa-pause');
            sessionStorage.setItem('isPlaying', 'true'); // Lưu trạng thái đang phát
        }
        isPlaying = !isPlaying;
    });

    // Next Button
    nextBtn.addEventListener('click', () => {
        currentSongIndex = (currentSongIndex + 1) % songList.length;
        loadSong(songList[currentSongIndex]);
        audioPlayer.play();
        isPlaying = true;
        playPauseBtn.classList.remove('fa-play');
        playPauseBtn.classList.add('fa-pause');
        sessionStorage.setItem('currentSongIndex', currentSongIndex); // Lưu lại bài hát hiện tại
        sessionStorage.setItem('isPlaying', 'true'); // Lưu trạng thái đang phát
    });

    // Previous Button
    prevBtn.addEventListener('click', () => {
        currentSongIndex = (currentSongIndex - 1 + songList.length) % songList.length;
        loadSong(songList[currentSongIndex]);
        audioPlayer.play();
        isPlaying = true;
        playPauseBtn.classList.remove('fa-play');
        playPauseBtn.classList.add('fa-pause');
        sessionStorage.setItem('currentSongIndex', currentSongIndex); // Lưu lại bài hát hiện tại
        sessionStorage.setItem('isPlaying', 'true'); // Lưu trạng thái đang phát
    });
	// Shuffle Button
    shuffleBtn.addEventListener('click', () => {
        isShuffle = !isShuffle;
        shuffleBtn.classList.toggle('active', isShuffle);
        shuffleBtn.classList.toggle('inactive', !isShuffle);
        sessionStorage.setItem('isShuffle', isShuffle.toString()); // Lưu trạng thái shuffle
        if (isShuffle) {
            songList = songList.sort(() => Math.random() - 0.5); // Xáo trộn danh sách
        } else {
            songList = songList.sort(); // Sắp xếp lại theo thứ tự ban đầu
        }
    });

    // Repeat Button
    repeatBtn.addEventListener('click', () => {
        isRepeat = !isRepeat;
        audioPlayer.loop = isRepeat; // Kích hoạt chế độ lặp
        repeatBtn.classList.toggle('active', isRepeat);
        repeatBtn.classList.toggle('inactive', !isRepeat);
        sessionStorage.setItem('isRepeat', isRepeat.toString()); // Lưu trạng thái repeat
    });

	
    // Volume control
    volumeControl.addEventListener('input', () => {
        const volume = volumeControl.value / 100;
        audioPlayer.volume = volume;
        sessionStorage.setItem('audioPlayerVolume', volume); // Lưu lại giá trị âm lượng
    });
	
    // Lưu lại vị trí phát nhạc cuối cùng vào sessionStorage khi tạm dừng
    audioPlayer.addEventListener('pause', () => {
        sessionStorage.setItem('audioPlayerCurrentTime', audioPlayer.currentTime);
        sessionStorage.setItem('isPlaying', 'false'); // Lưu trạng thái tạm dừng
    });
	
    // Lưu lại vị trí phát nhạc cuối cùng khi đóng trang
    window.addEventListener('beforeunload', () => {
        sessionStorage.setItem('audioPlayerCurrentTime', audioPlayer.currentTime);
        sessionStorage.setItem('isPlaying', isPlaying.toString()); // Lưu trạng thái phát nhạc
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const volumeControl = document.getElementById("volume-control");
    const volumeIcon = document.getElementById("volume-icon");
    const audioPlayer = document.getElementById("audio-player");

    // Lấy giá trị âm lượng đã lưu từ sessionStorage, nếu có
    const savedVolume = sessionStorage.getItem("audioPlayerVolume");
    if (savedVolume !== null) {
        volumeControl.value = savedVolume * 100; // Lấy lại giá trị âm lượng đã lưu
        audioPlayer.volume = savedVolume; // Cập nhật âm lượng của audio
    }

    let previousVolume = volumeControl.value; // Lưu giá trị âm lượng ban đầu

    // Lắng nghe sự kiện khi thay đổi âm lượng
    volumeControl.addEventListener("input", function () {
        // Thay đổi âm lượng của audio khi kéo thanh
        audioPlayer.volume = volumeControl.value / 100;
        sessionStorage.setItem("audioPlayerVolume", volumeControl.value / 100); // Lưu âm lượng vào sessionStorage
    });

    // Lắng nghe sự kiện khi nhấn vào biểu tượng loa
    volumeIcon.addEventListener("click", function () {
        if (volumeControl.value === "0") {
            // Nếu âm lượng hiện tại là 0, khôi phục lại giá trị âm lượng trước đó
            volumeControl.value = previousVolume;
            audioPlayer.volume = previousVolume / 100; // Cập nhật lại âm lượng của audio
            sessionStorage.setItem("audioPlayerVolume", previousVolume / 100); // Lưu lại âm lượng
        } else {
            // Lưu giá trị âm lượng trước khi đưa thanh về bên trái
            previousVolume = volumeControl.value;
            volumeControl.value = 0; // Đưa thanh âm lượng về 0
            audioPlayer.volume = 0; // Tắt âm thanh
            sessionStorage.setItem("audioPlayerVolume", 0); // Lưu lại âm lượng 0
        }
    });
});
