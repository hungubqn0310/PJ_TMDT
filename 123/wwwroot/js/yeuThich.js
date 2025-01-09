document.addEventListener('DOMContentLoaded', function() {
    // Fake data
    const products = Array.from({ length: 100 }, (_, index) => ({
        id: index + 1,
        name: `Trang sức ${index + 1}`,
        price: Math.floor(Math.random() * 10000000) + 1000000,
        originalPrice: Math.floor(Math.random() * 15000000) + 2000000,
        image: `https://picsum.photos/400/400?random=${index}`,
        category: ['Nhẫn', 'Dây chuyền', 'Lắc tay', 'Bông tai'][Math.floor(Math.random() * 4)],
        isFavorite: true // Set isFavorite mặc định là true
    }));
    let currentPage = 1;
    const itemsPerPage = 12;
    let currentView = 'grid';
    let currentSort = 'newest';
    // Functions
    function renderProducts(page) {
        const start = (page - 1) * itemsPerPage;
        const end = start + itemsPerPage;
        const productGrid = document.getElementById('productGrid');
        productGrid.innerHTML = '';
        const sortedProducts = sortProducts(products, currentSort);
        const pageProducts = sortedProducts.slice(start, end);
        pageProducts.forEach(product => {
            const card = document.createElement('div');
            card.className = 'product-card';
            card.innerHTML = `
                <div class="product-image">
                    <img src="${product.image}" alt="${product.name}" loading="lazy">
                    <div class="product-actions">
                        <button class="action-btn favorite-btn ${product.isFavorite ? 'active' : ''}">
                            <i class="fas fa-heart"></i>
                        </button>
                        <button class="action-btn cart-btn">
                            <i class="fas fa-shopping-cart"></i>
                        </button>
                    </div>
                </div>
                <div class="product-info">
                    <h3 class="product-name">${product.name}</h3>
                    <div class="product-category">${product.category}</div>
                    <div class="price-wrapper">
                        <span class="product-price">${formatPrice(product.price)}đ</span>
                        ${product.originalPrice > product.price ? 
                            `<span class="original-price">${formatPrice(product.originalPrice)}đ</span>` : ''}
                    </div>
                </div>
            `;
            // Add event listeners
            const favoriteBtn = card.querySelector('.favorite-btn');
            const cartBtn = card.querySelector('.cart-btn');
            // Favorite button click
            favoriteBtn.addEventListener('click', (e) => {
                e.preventDefault();
                favoriteBtn.classList.toggle('active'); // Toggle active class
                handleFavorite(product.id);
                showNotification(favoriteBtn.classList.contains('active') ? 'Đã thêm vào yêu thích' : 'Đã xóa khỏi danh sách yêu thích');
            });
            // Cart button click
            cartBtn.addEventListener('click', (e) => {
                e.preventDefault();
                cartBtn.classList.add('active'); // Add active class
                handleAddToCart(product);
                showNotification('Đã thêm vào giỏ hàng');
            });
            productGrid.appendChild(card);
        });
        renderPagination(Math.ceil(products.length / itemsPerPage));
        updateTotalItems();
    }
    function formatPrice(price) {
        return new Intl.NumberFormat('vi-VN').format(price);
    }
    function renderPagination(totalPages) {
        const pagination = document.getElementById('pagination');
        pagination.innerHTML = '';
        // Previous button
        if (currentPage > 1) {
            const prevBtn = createPageButton('‹', currentPage - 1);
            pagination.appendChild(prevBtn);
        }
        // Page numbers
        for (let i = 1; i <= totalPages; i++) {
            if (
                i === 1 ||
                i === totalPages ||
                (i >= currentPage - 2 && i <= currentPage + 2)
            ) {
                const pageBtn = createPageButton(i, i);
                if (i === currentPage) pageBtn.classList.add('active');
                pagination.appendChild(pageBtn);
            } else if (
                i === currentPage - 3 ||
                i === currentPage + 3
            ) {
                const dots = document.createElement('span');
                dots.className = 'pagination-dots';
                dots.textContent = '...';
                pagination.appendChild(dots);
            }
        }
        // Next button
        if (currentPage < totalPages) {
            const nextBtn = createPageButton('›', currentPage + 1);
            pagination.appendChild(nextBtn);
        }
    }
    function createPageButton(text, pageNum) {
        const button = document.createElement('button');
        button.className = 'page-btn';
        button.textContent = text;
        button.addEventListener('click', () => {
            currentPage = pageNum;
            renderProducts(currentPage);
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
        return button;
    }
    function sortProducts(products, sortType) {
        const sortedProducts = [...products];
        switch (sortType) {
            case 'price-asc':
                return sortedProducts.sort((a, b) => a.price - b.price);
            case 'price-desc':
                return sortedProducts.sort((a, b) => b.price - a.price);
            case 'name-asc':
                return sortedProducts.sort((a, b) => a.name.localeCompare(b.name));
            default:
                return sortedProducts;
        }
    }
    function showNotification(message) {
        const notification = document.getElementById('notification');
        const notificationText = document.getElementById('notificationText');
        notificationText.textContent = message;
        notification.classList.add('show');
        setTimeout(() => {
            notification.classList.remove('show');
        }, 3000);
    }
    function updateTotalItems() {
        const totalItems = document.getElementById('totalItems');
        const productCount = document.querySelectorAll('.product-card').length;
        totalItems.textContent = productCount;
        if (productCount === 0) {
            showEmptyState();
        }
    }
    function showEmptyState() {
        const productGrid = document.getElementById('productGrid');
        productGrid.innerHTML = `
            <div class="empty-state">
                <i class="far fa-heart"></i>
                <h2>Chưa có sản phẩm yêu thích</h2>
                <p>Hãy thêm những sản phẩm bạn yêu thích vào đây</p>
                <button onclick="window.location.href='/products'">
                    Khám phá sản phẩm
                </button>
            </div>
        `;
    }
    // Event Listeners
    document.getElementById('sortSelect').addEventListener('change', (e) => {
        currentSort = e.target.value;
        renderProducts(currentPage);
    });
    document.querySelectorAll('.view-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            document.querySelectorAll('.view-btn').forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
            currentView = btn.dataset.view;
            document.getElementById('productGrid').className = `product-grid ${currentView}-view`;
        });
    });
    // Initialize
    renderProducts(currentPage);
});